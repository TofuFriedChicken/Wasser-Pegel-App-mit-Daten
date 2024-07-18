using Microsoft.Maui.Maps;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Maps;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Maui.ApplicationModel;


namespace Pegel_Wetter_DFFUDC
{
    public partial class MainPage : ContentPage
    {

        WaterLevelModel _model;
        public bool _visiblePinsMaybe;
        public List<Pin> _pins;

        RainfallOpenDataModel _rainmodel;
        public MainPage()
        {
            InitializeComponent();

            var germanLocation = new Location(52.5200, 13.4050);     //center berlin + map adjustment
            var mapSpan = new MapSpan(germanLocation, 90.0, 180.0);
            germanMap.MoveToRegion(mapSpan);
            SizeAdjustment(this, EventArgs.Empty);

            _model = new WaterLevelModel();     // WaterLevel Pin
            BindingContext = _model;
            _visiblePinsMaybe = false;
            _pins = new List<Pin>();

        }

        public void SizeAdjustment(object sender, EventArgs e)
        {
            germanMap.WidthRequest = this.Width;
            germanMap.HeightRequest = this.Height;
        }



        private async void LoadWaterPins_Clicked(object sender, EventArgs e)     //  WaterLevel Pins
        {
            try
            {
                if (!_visiblePinsMaybe)
                {

                }
                if (_pins.Count == 0)
                {
                    await WaterButton();
                }
                foreach (var pin in _pins)
                {
                    germanMap.Pins.Add(pin);
                }
                _visiblePinsMaybe = true;

            }
            catch (Exception ex)
            {
                await DisplayAlert("Fehler beim Laden der Pins", ex.Message, "OK");
            }
        }
        private void RemovePins_Clicked(object sender, EventArgs e)         // remove all pins
        {
            if (_visiblePinsMaybe == true)
            {
                germanMap.Pins.Clear();
                _visiblePinsMaybe = false;
            }
        }
        private async Task WaterButton()
        {

            if (_pins.Count == 0)
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
                    _pins.Add(pin);
                }
            }


            //private async void LoadRainPins_Clicked(object sender, EventArgs e)       // Rain Pins
            //{
            //var viewModel = BindingContext as RainfallOpenDataViewModel;
            //await viewModel.LoadDataAsync();

            //germanMap.Pins.Clear();
            //foreach (var data in viewModel.RainfallData)
            //{
            //    var pin = new Pin
            //    {
            //        Label = data.Location,
            //        Address = $"Rainfall: {data.Rainfall}mm",
            //        Type = PinType.Place,
            //        Location = new Location(data.Latitude, data.Longitude)
            //    };
            //    germanMap.Pins.Add(pin);
            //}
            //}
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









