using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Maps;
using CommunityToolkit.Maui.Maps;
using System.Net.NetworkInformation;
using CommunityToolkit.Maui;
using static Pegel_Wetter_DFFUDC.swapDates;
using Map = Microsoft.Maui.Controls.Maps.Map;
using static Pegel_Wetter_DFFUDC.RainfallStation;
using CommunityToolkit.Maui.Views;
using System;


namespace Pegel_Wetter_DFFUDC
{
    public partial class swapDates : ContentPage
    {
        private DateTime today = DateTime.Today;
        Location location = new Location(52.5162, 13.3777); //current Berlin

        

        WaterLevelModel WlModel = new WaterLevelModel() ;
        private List<Pin> WlPinslist = new List<Pin>();
        
        RainfallModel RfModel = new RainfallModel(new RainfallApi()) ; //Api Key 
        private List<Pin> RfPinslist = new List<Pin>();
                
        public swapDates()
        {
            InitializeComponent();

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

        
        private void UpdateTodayLabel() { DateLabel.Text = today.ToString("dddd, dd. MMMM yyyy"); /*form changeable*/}

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
                //Unittest
                string currentDayPopUp = "You've reached the current date.";
                await DisplayAlert("Information", currentDayPopUp, "Exit");
            }
            else
            {
                today = today.AddDays(1); //1 day to future
                UpdateTodayLabel();
            }

        }

        //Quelle ChatGPT 4.0, 25.07.2024
        private async Task ShowLoadingPopup(Func<Task> loadDataFunc)
        {
            //Popup Ladefenster definiert
            var loadingPopup = new Popup
            {
                Content = new VerticalStackLayout
                {
                    Padding = new Thickness(20),
                    BackgroundColor = Colors.White,
                    Children = { new ActivityIndicator {IsRunning = true, Color = Colors.Black}, new Label { Text = "Pins werden geladen", TextColor = Colors.Black } }
                }
            };

            MainThread.BeginInvokeOnMainThread(() => { this.ShowPopup(loadingPopup); /*Popup aufgerufen*/ });

            try { await loadDataFunc(); }
            catch (Exception ex) { await MainThread.InvokeOnMainThreadAsync(async () => { await DisplayAlert("Fehler", "Es ist ein fehler aufgetreten. \n" + ex.Message, "OK"); }); }
            finally { MainThread.BeginInvokeOnMainThread(() => { loadingPopup.Close(); }); }
        }
      
        
//Rainfall      
        private async void rainfallmap_Clicked(object sender, EventArgs e) 
        {
            await ShowLoadingPopup(async () => { await AddPinsRainfall(); }); //soll Ladefenster öffnen
        }

        //getRainfall in Liste
        private async Task AddPinsRainfall() 
        {
            MyMap_Test.Pins.Clear();

            string url = "https://opendata.dwd.de/climate_environment/CDC/observations_germany/climate/daily/more_precip/historical/RR_Tageswerte_Beschreibung_Stationen.txt";
            var stations = await RfModel.GetRainStationsAsync(url);
            
            LoadRainPins(stations); //kein Datum notwendig, da keine Historie verfügbar
        }

        private void LoadRainPins(List<RainfallStation> stations)
        {
            foreach (var station in stations)
            {
                Pin RfPin = new Pin
                {
                    Label = station.Stationname,
                    Address = $"{station.State}, High: {station.StationHeight}m",
                    Location = new Location(station.Latitude, station.Longitude)
                };

                MyMap_Test.Pins.Add(RfPin);
            }
        }

//Waterlevel
        private async void waterlevelmap_Clicked(object sender, EventArgs e) 
        {
            await ShowLoadingPopup(async () =>
            {
                await AddPinsWaterlevel(); /* sollte aktuelles datum abfragen und aktualisieren*/
                foreach (Pin pin in WlPinslist)
                {
                    MyMap_Test.Pins.Add(pin);
                    pin.MarkerClicked += WlPin_Clicked;
                }
            });
        }

        //getWaterlevel in Listenform (von Sophie feature 1.1)
        private async Task AddPinsWaterlevel()
        {
            MyMap_Test.Pins.Clear();

            await LoadWaterPins();
        }

        private async Task LoadWaterPins()
        {
            try
            {
                await WlModel.LoadWaterLevels();

                foreach (var position in WlModel.Positions)
                {
                    //erstellt Pins und speichert sie in Liste
                    Pin wlPin = new Pin
                    {
                        Label = position.longname,
                        Address = position.agency,
                        Location = new Location(position.latitude, position.longitude)
                    };

                    WlPinslist.Add(wlPin);
                }
            }
            catch (Exception ex) { await DisplayAlert("Ladefehler", "Pins konnten nicht geladen werden \n" + ex.Message, "schließen"); }
        }

        private void WlPin_Clicked(object sender, PinClickedEventArgs e)
        {
            //Anzeige wenn auf Pin geklickt

            Pin pin = sender as Pin;
            var position = WlModel.Positions.FirstOrDefault(p => p.latitude == pin.Location.Latitude && p.longitude == pin.Location.Longitude);

            today = DateTime.Now;
            var details = $"Location: {position.water.longname.ToLower()} - {position.agency.ToLower()} \n" +
                          $"Value: {position.Timeseries[0].currentMeasurement.Value} cm \nDate: {today}";

            DisplayAlert("Waterlevelstation", details, "Schließen");

        }
    }
}