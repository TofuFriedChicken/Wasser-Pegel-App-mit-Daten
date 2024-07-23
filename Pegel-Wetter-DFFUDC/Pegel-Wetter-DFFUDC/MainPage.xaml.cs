<<<<<<< Pegel-Wetter-DFFUDC/Pegel-Wetter-DFFUDC/MainPage.xaml.cs
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
using static Pegel_Wetter_DFFUDC.RainfallStation;
using static Microsoft.Maui.ApplicationModel.Permissions;
using System.Reflection;
using CsvHelper;


//Code behind

namespace Pegel_Wetter_DFFUDC
{
    public partial class MainPage : ContentPage
    {
        WaterLevelModel _model;
        public bool _visiblePinsMaybe;
        private List<Pin> _loadedPins = new List<Pin>();    // list for the WaterPins

        private readonly RainfallModel _rainfallModel;

        private DateTime today = DateTime.Today;
        Location location = new Location(52.5162, 13.3777); //current Berlin

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

            // Rainfall Pin
            _rainfallModel = new RainfallModel(new RainfallApi());

            //startpoint, based on variable location
            MyMap_Test.MoveToRegion(MapSpan.FromCenterAndRadius(location, Distance.FromMeters(1000)));

            var circle = new Circle()
            {
                Center = location,
                Radius = Distance.FromMeters(100000),
                StrokeColor = Color.FromArgb("0000FF"),
                StrokeWidth = 8,
                FillColor = Color.FromArgb("ADD8E6")
            };
            
            MyMap_Test.MapElements.Add(circle);

            Pin testpin = new Pin
            {
                Label = "Berlin",
                Address = "testpin for messuring station",
                Type = PinType.Place,
                Location = new Location(52.5162, 13.3777)
            };

            MyMap_Test.Pins.Add(testpin);

            UpdateTodayLabel(); //current date 
        }

        private void CurrentMapSizeUpdate(object sender, EventArgs e)
        {
            //update Mapsize based on current windowsize
            MyMap_Test.WidthRequest = this.Width;
            MyMap_Test.HeightRequest = this.Height;
        }

        private void UpdateTodayLabel()
        {
            DateLabel.Text = today.ToString("dddd, dd. MMMM yyyy"); //form changeable
        }

        private void DateBack_Clicked(object sender, EventArgs e)
        {
            today = today.AddDays(-1); //1 day to past
            UpdateTodayLabel();
        }

        private async void DateForward_Clicked(object sender, EventArgs e)
        {
            //shouldn't see/click to future dates
            if (today == DateTime.Today)
            {
                string currentDayPopUp = "You've reached the current date.";
                await DisplayAlert("Information", currentDayPopUp, "Exit");
            }
            else
            {
                today = today.AddDays(1); //1 day to future
                UpdateTodayLabel();
            }

        }

        //getWaterlevel in Listenform (von Sophie feature 1.1)
        private void AddPinsWaterlevel()
        {
            MyMap_Test.Pins.Clear();

            //datum auslesen und nur tagesaktuelle daten in detailfenster anzeigen bei mouseover
            /*
            foreach (var item in collection)
            {
                Pins erzeugen
            }*/
        }

        private void rainfallmap_Clicked(object sender, EventArgs e)
        {
            //aktuelles datum abfragen und aktualisieren
            AddPinsWaterlevel();
        }

        //getWaterlevel in Listenform (von Sophie feature 1.1)
        private void AddPinsRainfall()
        {
            MyMap_Test.Pins.Clear();
        }

        private void waterlevelmap_Clicked(object sender, EventArgs e)
        {
            AddPinsRainfall();
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
                    //var key = pinData.Uuid;       
                    //if (!_pinDataCache.Contains(key))
                    //{
                    //    _pinDataCache.Add(key, pinData);
                    //}
                    var pin = new Pin
                    {
                        Label = position.longname,
                        Address = position.agency, // + hier den Value von CurrentMeasurement anzeigen
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
                pin.MarkerClicked += Pin_Clicked;
            }
            _visiblePinsMaybe = true;
        }

        private void Pin_Clicked(object sender, EventArgs e)
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

        private async void ShowRainPins(object sender, EventArgs e)           // Pins Rainfall
        {
            string url = "https://opendata.dwd.de/climate_environment/CDC/observations_germany/climate/daily/more_precip/historical/RR_Tageswerte_Beschreibung_Stationen.txt";
            var stations = await _rainfallModel.GetRainStationsAsync(url);
            LoadRainPins(stations);
        }


        private void LoadRainPins(List<RainfallStation> stations)
        {
            foreach (var station in stations)
            {
                var pin = new Pin
                {
                    Label = station.Stationname,
                    Address = $"{station.State}, High: {station.StationHeight}m",
                    Location = new Location(station.Latitude, station.Longitude)
                };

                germanMap.Pins.Add(pin); 
            }
        }


        // go to other Pages
        public async void GoCircleMode(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ExamplePage());
        }
        public async void GoCurrentData(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ExamplePage());
        }
        private async void GoAddData(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ExamplePage());
        }

        private async void GoHistorie(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ExamplePage());
        }

        private async void GoDiagram(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ExamplePage());
        }

    }
}