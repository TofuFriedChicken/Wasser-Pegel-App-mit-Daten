using Microsoft.Maui.Maps;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Maps;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Pegel_Wetter_DFFUDC
{
    public partial class MainPage : ContentPage
    {

        WaterLevelModel _model;
        RainfallOpenDataModel _rainmodel;
        public MainPage()
        {
            InitializeComponent();
            //center germany
            var germanLocation = new Location(51.1657,10.4515);
            var mapSpan = new MapSpan(germanLocation, 50.0, 50.0);
            germanMap.MoveToRegion(mapSpan);

            // pin for WaterLevel Stations
            _model = new WaterLevelModel();
            BindingContext = _model;
            LoadPins();
            // pin for Rainfall Stations
            _rainmodel = new RainfallOpenDataModel();
            LoadPinsW();

        }
        private async void LoadPins() // pin for WaterLevel
        {
            await _model.LoadWaterLevels();
            foreach (var position in _model.Positions)
            {
                var pin = new Pin
                {
                    Label = $"{position.water.longname}: {position.longname} cm",  // CurrentMeasurement.value
                    Address = position.agency,
                    Location = new Location(position.latitude, position.longitude),
                    //Type = PinType.Place,
                    //Icon = BitmapDescriptorFactory.FromBundle("water_4081759.png")  // versuch die Pins zu verändern
                };
                germanMap.Pins.Add(pin);
            }
        }

        public async void LoadPinsW()
        {
            await _rainmodel.LoadRainfall();
            foreach (var position in _rainmodel.Positions)
            {
                var pin = new Pin
                {
                    Label = $"Standort: {position.Stationsname}",  // CurrentMeasurement.value
                    Location = new Location(position.geoLaenge, position.geoBreite),
                };
                germanMap.Pins.Add(pin);
            }
        }
    }


}


