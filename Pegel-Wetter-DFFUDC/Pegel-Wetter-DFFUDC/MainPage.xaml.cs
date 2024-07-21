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


namespace Pegel_Wetter_DFFUDC
{
    public partial class MainPage : ContentPage
    {

        WaterLevelModel _model;
        public bool _visiblePinsMaybe;
        private List<Pin> _loadedPins = new List<Pin>();    // list for the WaterPins

        
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
                var loadPinTasks = _model.Positions.Select(async position =>
                //foreach (var position in _model.Positions)
                {
                    var pin = new Pin
                    {
                        Label = position.longname,
                        Address = position.agency,
                        Location = new Location(position.latitude, position.longitude),
                    };
                    _loadedPins.Add(pin);
                }).ToList();

                await Task.WhenAll(loadPinTasks);
             
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

            if (position.currentMeasurement != null)        // um den aktuellen Wert zu testen
            {
                var details = $"Value: {position.currentMeasurement?.Value ?? 0} cm\n";
                details += $"Date: {position.currentMeasurement.Timestamp.ToShortDateString()}";

                DisplayAlert("Waterlevel", details, "OK");
            }
            else
            {
                var details = $"No current values for {position.water.longname}: {position.agency}\n";
                DisplayAlert("Waterlevel", details, "OK");
            }
            //if (position.currentMeasurement.value != null)        // value der letzten 20 Tage
            //{
            //    //für die Bezeichnungen
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
            //}
            //else
            //{
            //    var details = $"No current values for {position.water.longname}: {position.agency}\n";
            //    DisplayAlert("Waterlevel", details, "OK");
            //}
        }
        private void RemovePins_Clicked(object sender, EventArgs e)         // remove all pins
        {
            if (_visiblePinsMaybe == true)
            {
                germanMap.Pins.Clear();
                _visiblePinsMaybe = false;
            }

        }

        private async void LoadRainPins(object sender, EventArgs e)       // Rain Pins
        {
            var viewModel = BindingContext as RainfallOpenDataModel;
            await viewModel.LoadDataAsync();

            germanMap.Pins.Clear();
            foreach (var data in viewModel.RainfallData)
            {
                var pin = new Pin
                {
                    Label = data.Label,
                    Address = $"Rainfall: {data.Value}mm",
                    //Type = PinType.Place,
                    Location = new Location(data.Latitude, data.Longitude)
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










