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
            var location = new Location(51,10);
            var mapSpan = new MapSpan(location, 500000000, 500000000);
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
                    Label = position.agency,
                    Address = position.water.longname,
                    Location = new Location(position.latitude, position.longitude)
                };
                germanMap.Pins.Add(pin);
            }
        }
    }


}


