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
            
            var germanLocation = new Location(52.5200,13.4050);     //center berlin
            var mapSpan = new MapSpan(germanLocation, 90.0, 180.0);
            germanMap.MoveToRegion(mapSpan);
            
            SizeAdjustment(this, EventArgs.Empty);      //map adjustment

            _model = new WaterLevelModel();     // pin for WaterLevel Stations
            BindingContext = _model;

            _rainmodel = new RainfallOpenDataModel();       // pin for Rainfall Stations
            BindingContext = _rainmodel;


        }

        public void SizeAdjustment(object sender, EventArgs e)  //map
        {
            germanMap.WidthRequest = this.Width;
            germanMap.HeightRequest = this.Height;
        }

        private async void LoadWaterPins(object sender, EventArgs e) // pin for WaterLevel
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

        //public async void LoadRainPins(object sender, EventArgs e)  // Rainfall
        //{
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
        //}
    }

    // change pins?
    //Type = PinType.Place,
    //Icon = BitmapDescriptorFactory.FromBundle("water_4081759.png")  // versuch die Pins zu verändern



}


