using Microsoft.Maui.Maps;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Maps;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System.Net.NetworkInformation;
using static Microsoft.Maui.ApplicationModel.Permissions;
using System.Reflection;
using CsvHelper;
using System.Globalization;
using System.IO.Compression;
using System;
using CommunityToolkit.Maui.Views;
using System.Text;


//Code behind

namespace Pegel_Wetter_DFFUDC
{
    public partial class MainPage : ContentPage
    {
        WaterLevelModel _model;
        public bool _visiblePinsMaybe;
        private List<Pin> _loadedPins = new List<Pin>();    // list for the WaterPins

        private readonly RainfallApi _rainfallApi; 
        private readonly RainfallModel _rainfallModel;

        public MainPage()
        {
            InitializeComponent();

            var germanLocation = new Location(52.5200, 13.4050);     //center berlin + map adjustment
            var mapSpan = new MapSpan(germanLocation, 90.0, 180.0);
            germanMap.MoveToRegion(mapSpan);
            SizeAdjustment(this, EventArgs.Empty);
            _visiblePinsMaybe = false;

            _model = new WaterLevelModel();     // WaterLevel Pin
            BindingContext = _model;
            LoadWaterPins();

            _rainfallApi = new RainfallApi();      // Rainfall Pin
            _rainfallModel = new RainfallModel();

        }

        private void OnOpenListClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TestList());
        }

        private void OnOpenInputFormClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new InputFormMeasurementData());
        }

        private void OnHistoryPageClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HistoryPage());
        }

        // Ab hier sind die Waterlevel Pins:
        private async void LoadWaterPins()     
        {
            try
            { 
                await _model.LoadWaterLevels();
                
                foreach (var position in _model.Positions)
                {
                
                    var pin = new CustomPin
                    {
                        Label = position.longname,
                        Address = position.agency, 
                        Location = new Location(position.latitude, position.longitude),
                        PinIcon = "waterlevel.png"
                    };
                    _loadedPins.Add(pin);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Fehler beim Laden der Pins", ex.Message, "OK");
            }
        }

        private async Task ShowLoadingPopup(Func<Task> loadDataFunc) // er erkennt bei mir ShowPopup und Close nicht
        {
            //var loadingPopup = new Popup
            //{
            //    Content = new VerticalStackLayout
            //    {
            //        Padding = new Thickness(50),
            //        BackgroundColor = Colors.White,
            //        Children = { new ActivityIndicator { IsRunning = true, Color = Colors.Black }, new Label { Text = "One second, pins are set", TextColor = Colors.Black } }
            //    }
            //};

            //MainThread.BeginInvokeOnMainThread(() => { this.ShowPopup(loadingPopup); /*Popup aufgerufen*/ });

            //try { await loadDataFunc(); }
            //catch (Exception ex) { await MainThread.InvokeOnMainThreadAsync(async () => { await DisplayAlert("Fehler", "Es ist ein Fehler aufgetreten. \nLaden nicht erfolgreich.\n" + ex.Message, "OK"); }); }
            //finally { MainThread.BeginInvokeOnMainThread(() => { loadingPopup.Close(); }); }
        }
   
        private async void ShowWaterPins(object sender, EventArgs e)    // vielleicht noch WaterPins in ShowWater Pins, abhängig von LoadingPopup
        {
            await WaterPins();
        }
        private async Task WaterPins()
        {
            foreach (var pin in _loadedPins)
            {

                germanMap.Pins.Add(pin);
                pin.MarkerClicked += WaterlevelValues_Clicked;
            }
            _visiblePinsMaybe = true;
        }

        private async void WaterlevelValues_Clicked(object sender, EventArgs e)
        {
            var pin = sender as Pin;
            var position = _model.Positions.FirstOrDefault(p => p.latitude == pin.Location.Latitude && p.longitude == pin.Location.Longitude);

            var currentDate = DateTime.Now.ToString("dd.MM.yyyy");
            var details = $"\nStandort: {position.water.longname.ToLower()} - {position.agency.ToLower()}\n";
            details += $"\nWert: {position.Timeseries[0].currentMeasurement.Value} cm \n\nDatum: {currentDate}";

            await DisplayAlert("Pegelstände", details, "Ok");   

        }

        private void RemovePins_Clicked(object sender, EventArgs e)         // remove all pins
        {
            if (_visiblePinsMaybe == true)
            {
                germanMap.Pins.Clear();
                _visiblePinsMaybe = false;
            }
        }

        // ab hier die Rainfall Pins
        private async void ShowRainPins(object sender, EventArgs e)
        {
            string url = "https://opendata.dwd.de/climate_environment/CDC/observations_germany/climate/daily/more_precip/recent/RR_Tageswerte_Beschreibung_Stationen.txt";
            string[] lines = await _rainfallApi.LoadFileFromUrlAsync(url);  // Methode in neuer Klasse
            var processedLines = _rainfallModel.ProcessLines(lines);
            
            AddPinsToMap(processedLines);
        }

        private void AddPinsToMap(RainfallViewModel[] stations)
        {
            foreach (var station in stations)
            {
                var pin = new Pin
                {
                    Label = $"{station.StationName}, Höhe: {station.StationHight}m",
                    Address = station.StationID.ToString(),
                    Location = new Location(station.Latitude, station.Longitude)
                };
                germanMap.Pins.Add(pin);
                string StationID = station.StationID.ToString();        
                pin.MarkerClicked += (sender, e) => RainfallValues_Clicked(sender, e, StationID);
            }
            _visiblePinsMaybe = true;
            if (stations.Length > 0)
            {
                var centerPosition = new Location(stations[0].Latitude, stations[0].Longitude);
                germanMap.MoveToRegion(MapSpan.FromCenterAndRadius(centerPosition, Distance.FromKilometers(100)));
            }
        }

        //Rainfall History - 20 Historical - nochmal Schritt für Schritt für einfach weiterverwenden
        private async void RainfallValues_Clicked(object sender, PinClickedEventArgs e, string Station_id)
        {
            try
            {
                string baseUrl = "https://opendata.dwd.de/climate_environment/CDC/observations_germany/climate/daily/more_precip/recent/";
                string zipFileName = $"tageswerte_RR_00{Station_id}_akt.zip";            // Erstellen des ZIP-Dateinamens basierend auf der ID
                string zipFileUrl = $"{baseUrl}{zipFileName}";                            // Erstellen der vollständigen URL zur ZIP-Datei
                string localZipFilePath = DownloadZipFile(zipFileUrl, zipFileName);         // Herunterladen der ZIP-Datei von der URL !

                // Überprüfen, ob die ZIP-Datei erfolgreich heruntergeladen wurde
                if (!File.Exists(localZipFilePath))
                {
                    await DisplayAlert("Fehler:", "Zip File konnte nicht runtergeladen werden", "Ok");
                    return;
                }
                ExtractAndReadLastFileInZip(localZipFilePath);      // Extrahieren und Lesen der letzten Datei im ZIP-Archiv
            }
            catch (Exception ex)
            {
                await DisplayAlert("Fehler ist aufgetreten:",$"{ex.Message}","Ok"); // falls eine Ausnahme auftritt - 
            }
        }

        private string DownloadZipFile(string fileUrl, string fileName)
        {
            string localPath = Path.Combine(Path.GetTempPath(), fileName);
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.DownloadFile(fileUrl, localPath);
                }
                //DisplayAlert("ZIP file downloaded successfully.","ok","close"); // Bestätigung, dass die ZIP-Datei erfolgreich heruntergeladen wurde
            }
            catch (Exception ex)
            {
                DisplayAlert("Fehler beim laden des Zip Files",$"{ex.Message}","Ok");       // Ausgabe einer Fehlermeldung, falls ein Fehler beim Herunterladen auftritt
                throw;
            }
            return localPath;
        }

        public void ExtractAndReadLastFileInZip(string zipFilePath)
        {
            try
            {
                using (ZipArchive archive = ZipFile.OpenRead(zipFilePath))
                {
                    if (archive.Entries.Count == 0)
                    {
                        DisplayAlert("Fehler:","Zip Archive sind leer.","Ok");  // Überprüfen, ob das ZIP-Archiv Einträge enthält - machmal gibt es keine Messwerte mehr
                        return;
                    }
                    ZipArchiveEntry lastEntry = archive.Entries[archive.Entries.Count - 1];   // Auswählen des letzten Eintrags im ZIP-Archiv - produkt_nieder_tag!
                    using (var reader = new StreamReader(lastEntry.Open()))                   
                    {
                        DisplayAlert($"Daten aus: ",$"{lastEntry.FullName}","Ok");

                        reader.ReadLine();
                        List<string> lastLines = new List<string>();
                        Queue<string> lineQueue = new Queue<string>();      // Liste der 20 Werte

                        while (reader.Peek() >= 0)
                        {
                            string line = reader.ReadLine();
                            lineQueue.Enqueue(line);
                            if (lineQueue.Count > 20) // Liest die letztes 20 Tage ein _ beginnend bei dem ersten also zb. 06.07
                            {
                                lineQueue.Dequeue();
                            }
                        }
                        lastLines = lineQueue.ToList();

                        List<string> displayMessages = new List<string>();  // um am ended die Werte gelistet ausgeben zu können
                        foreach (string l in lastLines)  
                        {
                            string[] values = l.Split(';');
                            
                            string currentDate = values[1];     // Speichern des aktuellen Datums (1 Spalte) und RS-Werts (3 Spalte)
                            string currentRSValue = values[3];

                            displayMessages.Add($"Datum: {currentDate}  –  Niederschlag: {currentRSValue}");
                            // var currentDate = DateTime.Now.ToString("dd.MM.yyyy"); <- für die Ausgabe
                        }
                        string finalMessage = string.Join(Environment.NewLine, displayMessages);
                        this.DisplayAlert("Niederschlag der letzten 20 Tage:", finalMessage, "Ok");
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert($"Fehler beim extrahieren und lesen der Zip-Datei: ",$"{ex.Message}","Ok");
                throw;
            }
    }

        // go to other Pages
        public async void GoCircleMode(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ExamplePage());
        }

        public async void GoCurrentData(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TestList());
        }
        private async void GoAddData(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InputFormMeasurementData());
        }

        private async void GoHistorie(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HistoryPage());
        }

        private async void GoDiagram(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateDiagram());
        }

        private void SizeAdjustment(object sender, EventArgs e)
        {
            //Update Map size based on current window size
            germanMap.WidthRequest = this.Width;
            germanMap.HeightRequest = this.Height;
        }

        private async void swapDatesBut_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new swapDates());
        }       
    }
}