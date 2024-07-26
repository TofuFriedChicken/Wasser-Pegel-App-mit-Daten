using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Maps;
using CommunityToolkit.Maui.Maps;
using System.Net.NetworkInformation;
using CommunityToolkit.Maui;
using static Pegel_Wetter_DFFUDC.swapDates;
using Map = Microsoft.Maui.Controls.Maps.Map;
using EventArgs = System.EventArgs;
using static Pegel_Wetter_DFFUDC.RainfallStation;
using static Pegel_Wetter_DFFUDC.RainfallApi;
using static Pegel_Wetter_DFFUDC.WaterLevelModel;
using static Pegel_Wetter_DFFUDC.WaterLevelViewModel;
using CommunityToolkit.Maui.Views;
using System;
using LiveChartsCore.Behaviours.Events;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Maps.Handlers;
using static SkiaSharp.HarfBuzz.SKShaper;
using Windows.System;
//using Microsoft.Maui.Controls.Compatibility.Platform.Android;
//using Microsoft.Maui.Controls.Compatibility.Platform.Android;
//using Javax.Security.Auth;


namespace Pegel_Wetter_DFFUDC
{
    public partial class swapDates : ContentPage
    {
        private DateTime today = DateTime.Today;
        Location location = new Location(52.5162, 13.3777); //current Berlin

        private DateTime date;

        //Wl
        WaterLevelModel WlModel = new WaterLevelModel();
        WaterLevelViewModel WlViewModel = new WaterLevelViewModel();
        private List<Pin> WlPinslist = new List<Pin>();
        

        //Rf Stationen 
        RainfallModel RfModel = new RainfallModel(new RainfallApi()); //Api Key
        RainfallApi RfApi = new RainfallApi();
        RainfallStations RfStations = new RainfallStations();
        private List<Pin> RfPinslist = new List<Pin>();

        string[] lines = { };

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
            //MyMap_Test.Pins.Add(testpin);

            UpdateTodayLabel(); //current date 
        }

        private void CurrentMapSizeUpdate(object sender, EventArgs e)
        {
            //update Mapsize based on current windowsize
            MyMap_Test.WidthRequest = this.Width;
            MyMap_Test.HeightRequest = this.Height;
        }

        private void UpdateTodayLabel() { DateLabel.Text = today.ToString("dddd, dd. MMMM yyyy"); /*form changeable*/}

        // normal auch wieder sichtbar machen, bis 20 tage zurück, bei gewähltem Datum und WlMap_Clicked auf daten des tages springen
        private void DateBack_Clicked(object sender, EventArgs e)
        {
            if (today <= (DateTime.Today - TimeSpan.FromDays(20)))
            {
                DisplayAlert("Fehler", "Daten werden nur von den letzten 20 Tagen abgerufen.", "Schließen");
            }
            else if (today > (DateTime.Today - TimeSpan.FromDays(20))) 
            {
                today = today.AddDays(-1); //1 day to past
                UpdateTodayLabel();

                LoadWlPinsDate(today);
            }
        }

        private async void DateForward_Clicked(object sender, EventArgs e)
        {
            //shouldn't see/click to future dates
            if (today == DateTime.Today)
            {
                //Unittest
                await DisplayAlert("Fehler", "Du bist beim aktuellen Datum angekommen.", "Schließen");
            }
            else
            {
                today = today.AddDays(1); //1 day to future
                UpdateTodayLabel();
                await LoadWlPinsDate(today); //leer bisher
            }

        }

        //Quelle ChatGPT 4.0, 25.07.2024; mitlerweile mehrfach verändert
        private async Task ShowLoadingPopup(Func<Task> loadedFunction)
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

            MainThread.BeginInvokeOnMainThread(() => { this.ShowPopup(loadingPopup); /*Popup aufgerufen*/ });

            try { await loadedFunction(); }
            catch (Exception ex) { await MainThread.InvokeOnMainThreadAsync(async () => { await DisplayAlert("Fehler", "Es ist ein Fehler aufgetreten. \nLaden nicht erfolgreich.\n" + ex.Message, "OK"); }); }
            finally { MainThread.BeginInvokeOnMainThread(() => { loadingPopup.Close(); }); }
        }


        //Rainfall      
        private async void rainfallmap_Clicked(object sender, EventArgs e)
        {
            await ShowLoadingPopup(async () => //soll Ladefenster öffnen
            {
                await AddPinsRainfall(); //fügt geladene pins aus API in Liste hinzu, Läd bis Funktion beendet
            }); 
        }

        //getRainfall in Liste -> lines(array)
        private async Task AddPinsRainfall()
        {
            MyMap_Test.Pins.Clear();

            //fragt stationen ab
            string url = "https://opendata.dwd.de/climate_environment/CDC/observations_germany/climate/daily/more_precip/historical/RR_Tageswerte_Beschreibung_Stationen.txt";
            string[] lines = await RfApi.LoadFileFromUrlAsync(url);

            //übergibt stationenarray an Methode, läd informationen in Liste aus stationen
            LoadRainPins(RfModel.ProcessLines(lines)); 
        }

        private void LoadRainPins(RainfallStations[] stations)
        {
            if (stations == null)
            {
                DisplayAlert("Fehler", "Die Liste ist leer. Daten können nicht ausgegeben werden", "Ok");
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

                        Label = $"{station.StationName}, Date: {station.FromDate} to {station.ToDate},\n" +
                                $"High: {station.StationHight}m,\n",
                        Address = station.StationID.ToString(),
                        Location = new Location(station.Latitude, station.Longitude) 
                    };

                    MyMap_Test.Pins.Add(RfPin);
                    //RfPin.MarkerClicked += RfPin_Clicked; //jeder Pin ein Clickevent

                }

                if (stations.Length > 0)
                {
                    var RfPinLocation = new Location(stations[0].Latitude, stations[0].Longitude);
                    MyMap_Test.MoveToRegion(MapSpan.FromCenterAndRadius(RfPinLocation, Distance.FromKilometers(100)));

                }
            } 
        }

        // Anzeige fehlerhaft, nicht vorhanden
        private void RfPin_Clicked(object sender, PinClickedEventArgs e) //Anzeige wenn auf Pin geklickt
        { 
            
            
            var Rflat = RfStations.Latitude; var Rflong = RfStations.Longitude;

            if (sender is Pin pin)
            {
                location = pin.Location;
                today = DateTime.Now;
                
                var details = $"Location: {Rflat} + {Rflong} \n" +
                              $"Value: 'Wert einfügen'  cm \n" +
                              $"Date: {today}";

                DisplayAlert("Waterlevelstation", details, "Schließen"); //deatils nicht korrekt ausgegeben

            }
        }


        //Waterlevel
        private async void waterlevelmap_Clicked(object sender, EventArgs e)
        {
            //zeigt Ladefenster solange (() => {Funktion}) läd     Lambda = (parameters) =>"goes to" {expression_or_statement_block} -> zum schreiben anonymer Funktionen (= Funktionen ohne Name, die gleich definiert werden, oft einmalig/kurzfristig)
            
            await ShowLoadingPopup(async () =>
            {   /* sollte aktuelles datum abfragen und aktualisieren*/

                //fügt geladene pins aus API in Liste hinzu
                await AddPinsWaterlevel();

                
            });
        }

        private async Task AddPinsWaterlevel()
        {
            MyMap_Test.Pins.Clear();

            await LoadWaterPins();
        }

        private async Task LoadWaterPins()
        {
            string testposition = "2.006"; //geht mit wert, Sophie fragen welche Variabe sinnvoll
            try
            {
                await WlModel.LoadWaterLevels();

                if (WlModel.Positions == null)
                {
                    await DisplayAlert("Fehler", "Die Liste ist leer. Daten können nicht ausgegeben werden", "Ok");
                }
                else if (WlModel.Positions != null)
                {
                    foreach (var position in WlModel.Positions) //fehler in deklarierung
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
                }
                
            }
            catch (Exception ex) { await DisplayAlert("Ladefehler", "Pins konnten nicht geladen werden \n" + ex.Message, "schließen"); }
        }

        //nicht aufgerufen, da bisher noch nicht funktioniert
        private async Task LoadWlPinsDate(DateTime date)
        {
            MyMap_Test.Pins.Clear();
            WlPinslist.Clear();

            //Abruf des Datums -> muss neue methode gebaut werden
            //await WlModel.LoadWaterLevels(date);

            foreach (var position in WlModel.Positions) //fehler bei back button
            {
                Pin wlPin = new Pin
                {
                    Label = position.longname,
                    Address = position.agency,
                    Location = new Location(position.longitude, position.latitude)
                };
                WlPinslist.Add(wlPin);
            }
        }

        private void WlPin_Clicked(object sender, PinClickedEventArgs e)
        {
            //Anzeige wenn auf Pin geklickt

            if (sender is Pin pin)
            {
                var position = WlModel.Positions.FirstOrDefault(p => p.latitude == pin.Location.Latitude && p.longitude == pin.Location.Longitude);
                today = DateTime.Now;
                var details = $"Location: {position.water.longname.ToLower()} - {position.agency.ToLower()} \n" +
                              $"Value: {position.Timeseries[0].currentMeasurement.Value} cm \nDate: {today}";

                DisplayAlert("Waterlevelstation", details, "Schließen");

            }


        }
    }
}

