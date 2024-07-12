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
            //center berlin
            var germanLocation = new Location(52.5200,13.4050);
            var mapSpan = new MapSpan(germanLocation, 90.0, 180.0);
            germanMap.MoveToRegion(mapSpan);
            //map adjustment
            SizeAdjustment(this, EventArgs.Empty);

            // pin for WaterLevel Stations
            _model = new WaterLevelModel();
            BindingContext = _model;
            LoadPins();
            // pin for Rainfall Stations
            _rainmodel = new RainfallOpenDataModel();
            LoadPinsW();
            

        }

        public void SizeAdjustment(object sender, EventArgs e)
        {
            germanMap.WidthRequest = this.Width;
            germanMap.HeightRequest = this.Height;
        }

        private async void LoadPins() // pin for WaterLevel
        {
            await _model.LoadWaterLevels();
            foreach (var position in _model.Positions)
            {
                var pin = new Pin
                {
                    Label = $"{position.water.longname}:", // {position.currentMeasurement.value} cm",  
                    Address = position.agency,
                    Location = new Location(position.latitude, position.longitude),
                    //Markericon = "waterlevel.png"
                };
                germanMap.Pins.Add(pin);
            }
        }

        public async void LoadPinsW()  // Rainfall
        {
            //await _rainmodel.LoadRainfall();
            //foreach (var position in _rainmodel.Positions)
            //{
            //    var pin = new Pin
            //    {
            //        Label = $"Standort: {position.Stationsname}",  
            //        Address = position.Stationsname,
            //        Location = new Location(position.geoLaenge, position.geoBreite),
            //    };
            //    germanMap.Pins.Add(pin);
            //}
        }
    }
    // change pins?
    //Type = PinType.Place,
    //Icon = BitmapDescriptorFactory.FromBundle("water_4081759.png")  // versuch die Pins zu verändern



}


