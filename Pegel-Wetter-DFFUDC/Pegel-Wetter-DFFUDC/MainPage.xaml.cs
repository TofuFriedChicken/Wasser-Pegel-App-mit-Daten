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

            _rainfallApi = new RainfallApi();   // Rainfall Pin
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
        private async Task ShowLoadingPopup(Func<Task> loadDataFunc)
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
   
        private async void ShowWaterPins(object sender, EventArgs e)
        {
            //await ShowLoadingPopup(async () => { await WaterPins(); });
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
            var details = $"Location: {position.water.longname.ToLower()} - {position.agency.ToLower()}\n";
            details += $"Value: {position.Timeseries[0].currentMeasurement.Value} cm \nDate: {currentDate}";

            await DisplayAlert("Waterlevel", details, "Close");     //await??? o mejor no

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
                    Label = $"{station.StationName}, High: {station.StationHight}m",
                    Address = station.StationID.ToString(),
                    Location = new Location(station.Latitude, station.Longitude)
                };
                germanMap.Pins.Add(pin);
                pin.MarkerClicked += RainfallValues_Clicked;
            }
            _visiblePinsMaybe = true;
            if (stations.Length > 0)
            {
                var centerPosition = new Location(stations[0].Latitude, stations[0].Longitude);
                germanMap.MoveToRegion(MapSpan.FromCenterAndRadius(centerPosition, Distance.FromKilometers(100)));
            }
        }

        //Rainfall History - 20 Days
        private async void RainfallValues_Clicked(object sender, PinClickedEventArgs e)
        {
            //string stationId = "00433"; //zum testen      // 2 Variante
            //var processor = new FileProcessor();
            //await processor.ProcessFileAsync(stationId);


            if (sender is Pin pin && !string.IsNullOrEmpty(pin.Address))      //zur sicherheit erstmal behalten
            {
                await RainValues(pin.Address); // um StationId als Argument in Adress
            }
            e.HideInfoWindow = true;
        }
        private async Task RainValues(string StationID)
        {
            try
            {
                var rsValues = await GetRSValuesAsync(StationID);
                string displayText = string.Join(Environment.NewLine, rsValues.Select(kvp => $"{kvp.Key}: {kvp.Value}"));
                await DisplayAlert($"RS Values for Station {StationID}", displayText, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private async Task<Dictionary<string, string>> GetRSValuesAsync(string StationID)
        {
            var rsValues = new Dictionary<string, string>();
            string baseUrl = "https://opendata.dwd.de/climate_environment/CDC/observations_germany/climate/daily/more_precip/recent/";
            string endDate = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
            string zipUrl = $"{baseUrl}produkt_nieder_tag_20230122_{endDate}_{StationID}.zip";

            using (var httpClient = new HttpClient())
            {
                //string zipUrl = $"{baseUrl}tageswerte_RR_{StationID}_akt.zip";
                byte[] zipBytes = await httpClient.GetByteArrayAsync(zipUrl);

                using (var zipStream = new MemoryStream(zipBytes))
                using (var archive = new ZipArchive(zipStream))
                {
                    foreach (var entry in archive.Entries)
                    {
                        if (entry.FullName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase) && entry.FullName.Contains("produkt"))
                        {
                            using (var reader = new StreamReader(entry.Open()))
                            {
                                while (!reader.EndOfStream)
                                {
                                    string line = await reader.ReadLineAsync();
                                    if (line.StartsWith("STATIONS_ID") || string.IsNullOrWhiteSpace(line))
                                        continue;

                                    var columns = line.Split(';');
                                    if (columns.Length >= 3 && DateTime.TryParseExact(columns[1], "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime date) && date >= DateTime.Now.AddDays(-20))
                                    {
                                        string dateString = date.ToString("yyyy-MM-dd");
                                        if (double.TryParse(columns[3], out double rsValue))
                                        {
                                            rsValues[dateString] = rsValue.ToString();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
            var last20Days = Enumerable.Range(0, 20).Select(offset => DateTime.Now.AddDays(-offset).ToString("yyyy-MM-dd")).ToList();
            foreach (var date in last20Days)
            {
                if (!rsValues.ContainsKey(date))
                {
                    rsValues[date] = "No Value";
                }
            }

            return rsValues;
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