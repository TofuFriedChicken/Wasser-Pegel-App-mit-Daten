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
        public MainPage()
        {
            InitializeComponent();
            //center germany
            var germanLocation = new Location(51.1657,10.4515);
            var mapSpan = new MapSpan(germanLocation, 10.0, 10.0);
            germanMap.MoveToRegion(mapSpan);

            _model = new WaterLevelModel();
            BindingContext = _model;
            LoadPins();

        }
        private async void LoadPins()
        {
            await _model.LoadWaterLevels();
            foreach (var position in _model.Positions)
            {
                var pin = new Pin
                {
                    Label = $"{position.water.longname}: {position.number} cm",  // CurrentMeasurement.value
                    Address = position.agency,
                    Location = new Location(position.latitude, position.longitude)
                };
                germanMap.Pins.Add(pin);
            }
        }
    }


}


