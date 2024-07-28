using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Maps;
using CommunityToolkit.Maui.Maps;
using System.Net.NetworkInformation;
using CommunityToolkit.Maui;
using static Pegel_Wetter_DFFUDC.swapDates;
using Map = Microsoft.Maui.Controls.Maps.Map;
using EventArgs = System.EventArgs;
using static Pegel_Wetter_DFFUDC.RainfallApi;
using static Pegel_Wetter_DFFUDC.RainfallModel;
using static Pegel_Wetter_DFFUDC.WaterLevelViewModel;
using static Pegel_Wetter_DFFUDC.MainPage;
using CommunityToolkit.Maui.Views;
using System;
using LiveChartsCore.Behaviours.Events;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Maps.Handlers;
using static SkiaSharp.HarfBuzz.SKShaper;
using System.Runtime.InteropServices;
using System.Linq;
//using Microsoft.Maui.Controls.Compatibility.Platform.Android;
//using Microsoft.Maui.Controls.Compatibility.Platform.Android;
//using Javax.Security.Auth;
//using Windows.System;

namespace Pegel_Wetter_DFFUDC
{
    /*
    public partial class swapDates : ContentPage
    {
        //Quelle für alles, wenn nicht anders angegeben: Microsoft Documentation

        private DateTime today = DateTime.Today;
        Location location = new Location(52.5162, 13.3777); //current Berlin

        private DateTime date;

        

        MainPage mainPage = new MainPage();

        //Rf

        RainfallModel RfModel = new RainfallModel();
        RainfallViewModel RfViewModel = new RainfallViewModel();
        RainfallApi RfApi = new RainfallApi(); //Api Key

        private List<Pin> RfPinslist = new List<Pin>();
        private List<string> RfData = new List<string>();

        private bool RfPinsVisible = false;

        //Wl

        WaterLevelViewModel WlViewModel = new WaterLevelViewModel();

        private List<Pin> WlPinslist = new List<Pin>();


        public swapDates()
        {
            InitializeComponent();

            //startpoint, based on variable location
            MyMap_swapDates.MoveToRegion(MapSpan.FromCenterAndRadius(location, Distance.FromMeters(1000)));


            //Testkreis, Aufgabe weggefallen
            var circle = new Circle()
            {
                Center = location,
                Radius = Distance.FromMeters(100000),
                StrokeColor = Color.FromArgb("0000FF"),
                StrokeWidth = 8,
                FillColor = Color.FromArgb("ADD8E6")
            };
            MyMap_swapDates.MapElements.Add(circle);

            Pin testpin = new Pin
            {
                Label = "Berlin",
                Address = "testpin for messuring station",
                Type = PinType.Place,
                Location = new Location(52.5162, 13.3777)
            };
            //MyMap_Test.Pins.Add(testpin);

            UpdateTodayLabel(); //current date 

            UpdateButtonVisible(); //nur sichtbar, wenn RainfallPins geladen

        }

        private void CurrentMapSizeUpdate(object sender, EventArgs e)
        {
            //update Mapsize based on current windowsize
            MyMap_swapDates.WidthRequest = this.Width;
            MyMap_swapDates.HeightRequest = this.Height;
        }

        private void UpdateButtonVisible()
        {
            if (RfPinsVisible == true)
            {
                // Macht Buttons klickbar
                DateBack.IsVisible = true;
                DateForward.IsVisible = true;
            }
            else if (RfPinsVisible == false)
            {
                DateBack.IsVisible = false;
                DateForward.IsVisible = false;
            }
        }

        private void UpdateTodayLabel() { DateLabel.Text = today.ToString("dddd, dd. MMMM yyyy"); /*form changeable}

        // normal auch wieder sichtbar machen, bis 20 tage zurck, bei gewhltem Datum und WlMap_Clicked auf daten des tages springen
        private async void DateBack_Clicked(object sender, EventArgs e)
        {
            if (today <= (DateTime.Today - TimeSpan.FromDays(20)))
            {
                await DisplayAlert("Fehler", "Daten werden nur von den letzten 20 Tagen abgerufen.", "Schlieen");
            }
            else if (today > (DateTime.Today - TimeSpan.FromDays(20)))
            {
                today = today.AddDays(-1); //1 day to past
                UpdateTodayLabel();

                //await LoadWlPinsDate(today);
                UpdateButtonVisible();
            }
        }

        private async void DateForward_Clicked(object sender, EventArgs e)
        {
            //shouldn't see/click to future dates
            if (today == DateTime.Today)
            {
                //Unittest
                await DisplayAlert("Fehler", "Du bist beim aktuellen Datum angekommen.", "Schlieen");
            }
            else
            {
                today = today.AddDays(1); //1 day to future
                UpdateTodayLabel();

                //await LoadWlPinsDate(today); //ld Pins fr aktuelles datum
                UpdateButtonVisible(); //aktualisiert Sichtbarkeit Buttons
            }
        }

        //Quelle ChatGPT 4.0, 25.07.2024; mitlerweile mehrfach verndert
        public async Task ShowLoadingPopup(Func<Task> loadedFunction)
        {
            //Popup Ladefenster definiert
            var loadingPopup = new Popup
            {
                Content = new VerticalStackLayout
                {
                    Padding = new Thickness(50),
                    BackgroundColor = Colors.White,
                    Children = { new ActivityIndicator { IsRunning = true, Color = Colors.Black }, new Label { Text = "Pins werden geladen", TextColor = Colors.Black } }
                }
            };

            MainThread.BeginInvokeOnMainThread(() => { this.ShowPopup(loadingPopup); /*Popup aufgerufen });

            try { await loadedFunction(); }
            catch (Exception ex) { await MainThread.InvokeOnMainThreadAsync(async () => { await DisplayAlert("Fehler", "Es ist ein Fehler aufgetreten. \nLaden nicht erfolgreich.\n" + ex.Message, "OK"); }); }
            finally { MainThread.BeginInvokeOnMainThread(() => { loadingPopup.Close(); }); }
        }


        //Rainfall      
        private async void rainfallmap_Clicked(object sender, EventArgs e)
        {
            RfPinsVisible = true;
            UpdateButtonVisible();

            await ShowLoadingPopup(async () => //soll Ladefenster ffnen
            {
                await AddPinsRainfall(); //fgt geladene pins aus API in Liste hinzu, Ld bis Funktion beendet
            });
        }

        private void reloadRFPinlist()
        {
            var pins = mainPage.LoadedPinsW;
            if (pins == null)
            {
                DisplayAlert("Fehler", "Die Liste ist leer. Daten knnen nicht ausgegeben werden", "Ok");
                return;
            }
            else if (pins != null)
            {
                foreach (var pin in pins)
                {
                    //testwerte
                    double latitudetest = 48.75845;
                    double longitudetest = 9.9855;

                    Pin RfPin = new Pin
                    {

                        Label = $"{pin.},\n",
                        Address = pin.StationID.ToString(),
                        Location = new Location(pin.Latitude, pin.Longitude)
                    };

                    RfPinslist.Add(RfPin);

                    string StationID = pin.StationID.ToString();
                    //RfPin.MarkerClicked += (sender, e) => mainPage.RainfallValues_Clicked(sender, e, StationID); 
                }

                foreach (var RfPin in RfPinslist) //Fehler, hängt sich mit haltepunkten auf, reagiert nivht mehr
                {
                    MyMap_swapDates.Pins.Add(RfPin);   // Zeigt Pins nicht an
                }
            }
        }

        
        private async Task AddPinsRainfall()
        {
            //fragt stationen ab
            string url = "https://opendata.dwd.de/climate_environment/CDC/observations_germany/climate/daily/more_precip/historical/RR_Tageswerte_Beschreibung_Stationen.txt";
            string[] lines = await RfApi.LoadFileFromUrlAsync(url); //Array von Arrays sinvoller oder Liste

            //bergibt stationenarray an Methode, ld informationen in Liste aus stationen
            LoadRainPins(RfViewModel.ProcessLines(lines));
        }

        private void LoadRainPins(RainfallModel[] stations)
        {
            MyMap_swapDates.Pins.Clear();
            RfPinslist.Clear();

            RfPinsVisible = true;

            if (stations == null)
            {
                DisplayAlert("Fehler", "Die Liste ist leer. Daten knnen nicht ausgegeben werden", "Ok");
                return;
            }
            else if (stations != null)
            {
                foreach (var station in stations)
                {
                    //testwerte
                    double latitudetest = 48.75845;
                    double longitudetest = 9.9855;

                    Pin RfPin = new Pin
                    {

                        Label = $"{station.StationName},\n",
                        Address = station.StationID.ToString(),
                        Location = new Location(station.Latitude, station.Longitude)
                    };

                    RfPinslist.Add(RfPin);

                    string StationID = station.StationID.ToString();
                    //RfPin.MarkerClicked += (sender, e) => mainPage.RainfallValues_Clicked(sender, e, StationID); 
                }

                foreach (var RfPin in RfPinslist) //Fehler, hängt sich mit haltepunkten auf, reagiert nivht mehr
                {
                    MyMap_swapDates.Pins.Add(RfPin);   // Zeigt Pins nicht an
                }
            }
        }

        // Anzeige fehlerhaft/nicht vorhanden, daher Methode von Sophie
        private void RfPin_Clicked(object sender, PinClickedEventArgs e, string StationID) //Anzeige wenn auf Pin geklickt
        {
            foreach (var pin in RfPinslist)
            {
                if (pin != null)
                {
                    var details = $"Location: {pin.Address} \n" +
                              $"Value: 'Wert einfgen'  cm \n" +    //nicht vollstndig
                              $"Date: {today}";

                    DisplayAlert("Waterlevelstation", details, "Schlieen"); //deatils nicht korrekt ausgegeben
                }
            }
        }


        //Waterlevel
        private async void waterlevelmap_Clicked(object sender, EventArgs e)
        {
            RfPinsVisible = false;
            UpdateButtonVisible();

            //zeigt Ladefenster solange (() => {Funktion}) ld     Lambda = (parameters) =>"goes to" {expression_or_statement_block} -> zum schreiben anonymer Funktionen (= Funktionen ohne Name, die gleich definiert werden, oft einmalig/kurzfristig)

            await ShowLoadingPopup(async () =>
            {   /* sollte aktuelles datum abfragen und aktualisieren

                //fgt geladene pins aus API in Liste hinzu
                await AddPinsWaterlevel();

            });
        }

        private async Task AddPinsWaterlevel()
        {
            await LoadWaterPins();
        }

        
        private async Task LoadWaterPins()
        {
            //testwerte
            string testposition = "2.006";

            try
            {
                await WlViewModel.LoadWaterLevels();

                if (WlViewModel.Positions == null)
                {
                    await DisplayAlert("Fehler", "Die Liste ist leer. Daten knnen nicht ausgegeben werden", "Ok");
                    return;
                }

                WlPinslist.Clear();

                foreach (var position in WlViewModel.Positions) //fehler in deklarierung
                {
                    //erstellt Pins und speichert sie in Liste
                    Pin WlPin = new Pin
                    {
                        Label = position.longname,
                        Address = position.agency,
                        Location = new Location(position.latitude, position.longitude)
                    };

                    WlPinslist.Add(WlPin);
                    WlPin.MarkerClicked += WlPin_Clicked; //jeder Pin ein Clickevent
                }

                foreach (var pin in WlPinslist)
                {
                    MyMap_swapDates.Pins.Add(pin);
                }
            }
            catch (Exception ex) { await DisplayAlert("Ladefehler", "Pins konnten nicht geladen werden \n" + ex.Message, "schlieen"); }

            // Pins zur Karte hinzufgen
            foreach (var WlPin in WlPinslist)
            {
                MyMap_swapDates.Pins.Add(WlPin);
            }
        }

        private void WlPin_Clicked(object sender, PinClickedEventArgs e)
        {
            //Anzeige wenn auf Pin geklickt

            if (sender is Pin pin)
            {
                var position = WlViewModel.Positions.FirstOrDefault(p => p.latitude == pin.Location.Latitude && p.longitude == pin.Location.Longitude);

                if (position != null)
                {
                    var currentDate = DateTime.Now.ToString("dd.MM.yyyy");
                    var details = $"Standort: {position.latitude} + {position.longitude} \n" +
                                  $"Wert: {position.currentMeasurement.Value} cm \nDatum: {position.currentMeasurement.Timestamp}";

                    DisplayAlert("Waterlevelstation", details, "Schlieen");
                }
            }
        }
    }
    */
}