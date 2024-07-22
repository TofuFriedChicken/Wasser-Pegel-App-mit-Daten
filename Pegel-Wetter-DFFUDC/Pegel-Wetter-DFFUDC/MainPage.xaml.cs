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
using static Pegel_Wetter_DFFUDC.RainfallOpenDataViewModel;
using static Microsoft.Maui.ApplicationModel.Permissions;
using System.Reflection;
using CsvHelper;


namespace Pegel_Wetter_DFFUDC
{
    public partial class MainPage : ContentPage
    {

        WaterLevelModel _model;
        public bool _visiblePinsMaybe;
        private List<Pin> _loadedPins = new List<Pin>();    // list for the WaterPins

        private RainfallOpenDataModel _modelData;

        
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

            //BindingContext = new RainfallOpenDataModel();   // Rainfall Pin
            _modelData = new RainfallOpenDataModel();
        }

        public void SizeAdjustment(object sender, EventArgs e)
        {
            germanMap.WidthRequest = this.Width;
            germanMap.HeightRequest = this.Height;
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


            //    //für die Bezeichnungen          // value der letzten 20 Tage??? 
            //    var lastDays = position.timeseries
            //        .Where(m => m.timestamp.Date >= DateTime.Today.AddDays(-20))
            //        .OrderByDescending(m => m.timestamp)
            //        .ToList();

            //    var details = $"Values for {position.water.longname}: {position.agency}\n";
            //    foreach (var measurement in lastDays)
            //    {
            //        //details += $"Date: {position.currentMeasurement.timestamp.ToShortDateString()}: {position.currentMeasurement.value} cm\n";
            //        details += $"{measurement.timestamp.ToShortDateString()}: {measurement.value} cm\n";
            //    }
            //    DisplayAlert("Location and Values", details, "Close");

        }
        private void RemovePins_Clicked(object sender, EventArgs e)         // remove all pins
        {
            if (_visiblePinsMaybe == true)
            {
                germanMap.Pins.Clear();
                _visiblePinsMaybe = false;
            }

        }

  
    
        private void ShowRainPins(object sender, EventArgs e)
        {
            string zipFilePath = "Resource/Raw/RainfallData.zip";       // nicht korrekt?
            string extractPath = "Raw";
            
            _modelData.LoadData(zipFilePath, extractPath);
             AddRainPinsToMap();
        }

        private void AddRainPinsToMap()
        {
            foreach (var pinData in _modelData.RainfallDataCollection)
            {
                var pin = new Pin
                {
                    //Type = PinType.Place,
                    Location = new Location(pinData.Latitude, pinData.Longitude),
                    Label = pinData.StationName,
                    Address = $"Station ID: {pinData.StationID}, Höhe: {pinData.StationHeight}m"
                };
                germanMap.Pins.Add(pin);
            }
            _visiblePinsMaybe = true;
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










