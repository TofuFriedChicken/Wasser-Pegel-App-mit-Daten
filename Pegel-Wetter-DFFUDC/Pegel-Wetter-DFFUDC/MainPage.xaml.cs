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


//Code behind

namespace Pegel_Wetter_DFFUDC
{
    public partial class MainPage : ContentPage
    {
        WaterLevelModel _model;
        public bool _visiblePinsMaybe;
        private List<Pin> _loadedPins = new List<Pin>();    // list for the WaterPins

        private readonly RainfallApi _rainfallApi;  // Rainfall
        private readonly RainfallModel _rainfallModel;

        public MainPage()
        {
            InitializeComponent();

            var germanLocation = new Location(52.5200, 13.4050);     //center berlin + map adjustment
            var mapSpan = new MapSpan(germanLocation, 90.0, 180.0);
            germanMap.MoveToRegion(mapSpan);
            SizeAdjustment(this, EventArgs.Empty);

            _model = new WaterLevelModel();     // WaterLevel Pin
            BindingContext = _model;
            LoadWaterPins();
            _visiblePinsMaybe = false;

            _rainfallApi = new RainfallApi(); // Rainfall Pin
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


        private async void LoadWaterPins()     //  WaterLevel Pins
        {
            try
            { 
                await _model.LoadWaterLevels();
                
                foreach (var position in _model.Positions)
                {
                
                    var pin = new Pin
                    {
                        Label = position.longname,
                        Address = position.agency, 
                        Location = new Location(position.latitude, position.longitude),
                      
                    };
                    _loadedPins.Add(pin);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Fehler beim Laden der Pins", ex.Message, "OK");
            }
        }

        private async void ShowWaterPins(object sender, EventArgs e)
        {
            foreach (var pin in _loadedPins)
            {

                germanMap.Pins.Add(pin);
                pin.MarkerClicked += WaterlevelValues_Clicked;
            }
            _visiblePinsMaybe = true;
        }

        private void WaterlevelValues_Clicked(object sender, EventArgs e)
        {
            var pin = sender as Pin;
            var position = _model.Positions.FirstOrDefault(p => p.latitude == pin.Location.Latitude && p.longitude == pin.Location.Longitude);

            var currentDate = DateTime.Now.ToString("dd.MM.yyyy");
            var details = $"Location: {position.water.longname.ToLower()} - {position.agency.ToLower()}\n";
            details += $"Value: {position.Timeseries[0].currentMeasurement.Value} cm \nDate: {currentDate}";

            DisplayAlert("Waterlevel", details, "Close");

        }

        private void RemovePins_Clicked(object sender, EventArgs e)         // remove all pins
        {
            if (_visiblePinsMaybe == true)
            {
                germanMap.Pins.Clear();
                _visiblePinsMaybe = false;
            }
        }

        private async void ShowRainPins(object sender, EventArgs e)
        {
            string url = "https://opendata.dwd.de/climate_environment/CDC/observations_germany/climate/daily/more_precip/recent/RR_Tageswerte_Beschreibung_Stationen.txt";
            string[] lines = await _rainfallApi.LoadFileFromUrlAsync(url);  // Methode in neuer Klasse
            var processedLines = _rainfallModel.ProcessLines(lines);
            AddPinsToMap(processedLines);
        }

        private void AddPinsToMap(RainfallStations[] stations)
        {
            foreach (var station in stations)
            {
                var pin = new Pin
                {
                    Label = station.StationName,
                    Address = $"ID: {station.StationID}, High: {station.StationHight}m",
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

        //Rainfall - 20 Days
        private async void RainfallValues_Clicked(object sender, PinClickedEventArgs e)
        {
            await RainValue();
            e.HideInfoWindow = true;
        }
        private async Task RainValue()
        {
            try
            {
                var rsValues = await GetRSValuesAsync();
                string displayText = string.Join(Environment.NewLine, rsValues.Select(kvp => $"{kvp.Key}: {kvp.Value}"));
                await DisplayAlert("RS Values", displayText, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private async Task<Dictionary<string, double>> GetRSValuesAsync()
        {
            var rsValues = new Dictionary<string, double>();

            using (var httpClient = new HttpClient())
            {
                for (int i = 1; i <= 10; i++)
                {
                    string zipUrl = $"https://opendata.dwd.de/climate_environment/CDC/observations_germany/climate/daily/more_precip/recent/tageswerte_RR_0000{i:00}_hist.zip";
                    byte[] zipBytes = await httpClient.GetByteArrayAsync(zipUrl);

                    using (var zipStream = new MemoryStream(zipBytes))
                    using (var archive = new ZipArchive(zipStream))
                    {
                        foreach (var entry in archive.Entries)
                        {
                            if (entry.FullName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
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
                                            if (double.TryParse(columns[3], out double rsValue))
                                            {
                                                rsValues[date.ToString("yyyy-MM-dd")] = rsValue;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
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