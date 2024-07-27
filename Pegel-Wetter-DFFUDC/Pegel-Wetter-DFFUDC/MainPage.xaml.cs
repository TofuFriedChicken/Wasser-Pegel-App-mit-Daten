using Microsoft.Maui.Maps;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Maps;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System.Net.NetworkInformation;
using static Microsoft.Maui.ApplicationModel.Permissions;
using System.IO.Compression;
using System;
using CommunityToolkit.Maui.Views;
using System.Text;
using System.Net.Http;
using System.Reflection;


//Code behind

namespace Pegel_Wetter_DFFUDC
{
    public partial class MainPage : ContentPage
    {
        WaterLevelModel _model;
        public bool _visiblePinsBoth;
        private List<Pin> _loadedPinsW = new List<Pin>();    // list for the WaterPins

        private readonly RainfallApi _rainfallApi; 
        private readonly RainfallModel _rainfallModel;

        public MainPage()
        {
            InitializeComponent();

            var germanLocation = new Location(52.5200, 13.4050);     //center berlin + map adjustment
            var mapSpan = new MapSpan(germanLocation, 90.0, 180.0);
            germanMap.MoveToRegion(mapSpan);
            SizeAdjustment(this, EventArgs.Empty);
            _visiblePinsBoth = false;

            _model = new WaterLevelModel();     // WaterLevel Pin
            BindingContext = _model;
            CreateWaterPins();

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

        // Waterlevel Pins:
        private async void CreateWaterPins()     
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
                        ImageSource = ImageSource.FromResource("Pegel_Wetter_DFFUDC.Recources.Images.waterlevel.png")
                    };
                    _loadedPinsW.Add(pin);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Fehler beim Laden der Pins", ex.Message, "OK");
            }
        }

        public async void ShowWaterPins(object sender, EventArgs e)   // Complicated alternative after the eternal failure of pop-ups and mop-ups and GIFS
        {
            var modalPage = new ContentPage
            {
                Content = new StackLayout
                {
                    Padding = new Thickness(60),
                    BackgroundColor = Colors.LightBlue,
                    Children =
                {
                    new ActivityIndicator { IsRunning = true, Color = Colors.Black },
                    new Label { Text = "Keine Sorgen, gleich geht es weiter. \n" +
                    "Diese Seite sehen Sie nur bis alle Messstationen zu den deutschlandweiten Pegelständen gesetzt sind. " +
                    "\nDas kann ein wenig dauern, wir haben alles unter Kontrolle." },
                    new Image
                    {
                        Source = "maploaded.png",
                        WidthRequest = 350, 
                        HeightRequest = 350,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center
                    }
                    // Quelle: /www.flaticon.com/free-sticker/map_9821275
                }
                }
            };
            await Navigation.PushModalAsync(modalPage);

            await Task.Delay(2000);     
            await Navigation.PopModalAsync();
            await WaterPins();

        }
     
        private async Task WaterPins()
        {
            foreach (var pin in _loadedPinsW)
            {

                germanMap.Pins.Add(pin);
                pin.MarkerClicked += WaterlevelValues_Clicked;
            }
            _visiblePinsBoth = true;
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
            if (_visiblePinsBoth == true)
            {
                germanMap.Pins.Clear();
                _visiblePinsBoth = false;
            }
        }

        // Rainfall Pins:
        public async void LoadRainPins(object sender, EventArgs e)  //loading Page
        {
            var modalPage = new ContentPage
            {
                Content = new StackLayout
                {
                    Padding = new Thickness(60),
                    BackgroundColor = Colors.LightSteelBlue,
                    Children =
                {
                    new ActivityIndicator { IsRunning = true, Color = Colors.Black },
                    new Label { Text = "Keine Sorgen, gleich geht es weiter. \n" +
                    "Diese Seite sehen Sie nur bis alle Messstationen zu den deutschlandweiten Niederschlägen gesetzt sind. " +
                    "\nGleich geht es weiter, wir haben alles unter Kontrolle." },
                    new Image
                    {
                        Source = "loading_pic.PNG",
                        WidthRequest = 400,
                        HeightRequest = 400,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Start
                    }
                    // Quelle: www.pinterest.de/pin/848224911048205613
                }
                }
            };
            await Navigation.PushModalAsync(modalPage);

            await Task.Delay(3000);                 // der muss noch etwas länger bleiben
            await Navigation.PopModalAsync();
            await RainPins();

        }
        private async Task RainPins()
        {
            string url = "https://opendata.dwd.de/climate_environment/CDC/observations_germany/climate/daily/more_precip/recent/RR_Tageswerte_Beschreibung_Stationen.txt";
            string[] lines = await _rainfallApi.LoadFileFromUrlAsync(url);  // Methode in neuer Klasse ??
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
            _visiblePinsBoth = true;
            if (stations.Length > 0)
            {
                var centerPosition = new Location(stations[0].Latitude, stations[0].Longitude);
                germanMap.MoveToRegion(MapSpan.FromCenterAndRadius(centerPosition, Distance.FromKilometers(100)));
            }
        }

        //Rainfall History - 20 Historical 
        private async void RainfallValues_Clicked(object sender, PinClickedEventArgs e, string Station_id)
        {
            try
            {
                string baseUrl = "https://opendata.dwd.de/climate_environment/CDC/observations_germany/climate/daily/more_precip/recent/";
                string zipFileName = $"tageswerte_RR_00{Station_id}_akt.zip";            
                string zipFileUrl = $"{baseUrl}{zipFileName}";                           
                string localZipFilePath = DownloadZipFile(zipFileUrl, zipFileName);        

                if (!File.Exists(localZipFilePath))
                {
                    await DisplayAlert("Fehler:", "Hier gibt es keine aktuellen Daten zu", "Ok");
                    return;
                }
                ExtractAndReadLastFileInZip(localZipFilePath);    
            }
            catch (Exception ex)
            {
                await DisplayAlert("Fehler ist aufgetreten:",$"{ex.Message}","Ok"); 
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
            }
            catch (Exception ex)
            {
                DisplayAlert("Fehler beim laden des Zip Files",$"{ex.Message}","Ok");    
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
                        DisplayAlert("Fehler:","Zip Archive sind leer.","Ok"); 
                        return;
                    }
                    ZipArchiveEntry lastEntry = archive.Entries[archive.Entries.Count - 1];  
                    using (var reader = new StreamReader(lastEntry.Open()))                   
                    {
                        DisplayAlert($"Daten aus: ",$"{lastEntry.FullName}","Ok");

                        reader.ReadLine();
                        List<string> lastLines = new List<string>();
                        Queue<string> lineQueue = new Queue<string>();      // List of 20 recent values

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

                        List<string> displayMessages = new List<string>(); 
                        foreach (string l in lastLines)  
                        {
                            string[] values = l.Split(';');
                            
                            string currentDate = values[1];     // Save current date (1 column) and RS value (3 column)
                            string currentRSValue = values[3];

                            displayMessages.Add($"Datum: {currentDate}  –  Niederschlag: {currentRSValue}");
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