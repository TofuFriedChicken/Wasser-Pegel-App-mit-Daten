using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Microsoft.Maui.ApplicationModel;
using System.Net.NetworkInformation;

//Code behind
namespace Pegel_Wetter_DFFUDC
{
    public partial class MainPage : ContentPage
    {
        private DateTime today = DateTime.Today;
        Location location = new Location(52.5162, 13.3777); //current Berlin

        public MainPage()
        {
            InitializeComponent();

            //startpoint, based on variable location
            MyMap_Test.MoveToRegion(MapSpan.FromCenterAndRadius(location, Distance.FromMeters(1000)));

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

    }

}


