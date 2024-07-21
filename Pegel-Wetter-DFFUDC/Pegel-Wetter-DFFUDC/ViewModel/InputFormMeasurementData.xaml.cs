
using System.Collections.ObjectModel;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.InteropServices;
using Pegel_Wetter_DFFUDC.Model;
using System.Windows.Input;

namespace Pegel_Wetter_DFFUDC;

public partial class InputFormMeasurementData : ContentPage
{

    public ObservableCollection<InputRainfallData> ListRainfallStation { get; set; }

    public ObservableCollection<InputWaterlevelData> ListWaterlevelStation { get; set; }

    public ObservableCollection<ModelInputintoHistory> ListHistory { get; set; }

    public ICommand OptionCommand { get; }

    public InputFormMeasurementData()
    {
        ListRainfallStation = new ObservableCollection<InputRainfallData>
            {
              new InputRainfallData { datatype="jojo", measurementStationName = "Alice", lon=4, lat=256, date=12, information="6", measurementData=2},
              new InputRainfallData { datatype="jojo", measurementStationName = "Alice", lon=4, lat=256, date=12, information="6", measurementData=2},
              new InputRainfallData { datatype="jojo", measurementStationName = "Alice", lon=4, lat=256, date=12, information="6", measurementData=2},
        };


        ListWaterlevelStation = new ObservableCollection<InputWaterlevelData>()
        {
              new InputWaterlevelData {datatype="jojo", measurementStationName = "hehe", lon = 3, lat = 123, date = 1123, information = "123", measurementData = 123 },
        };
        InitializeComponent();

        BindingContext = this;



    }


    private void OnPickerSelectedIndexChanged(object sender, EventArgs e)     //source eventhandler https://learn.microsoft.com/de-de/dotnet/maui/user-interface/controls/picker?view=net-maui-8.0 (last visit: 30.06.24)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1)
        {
            string selectedItem = (string)picker.ItemsSource[selectedIndex];

            if (selectedItem == "Pegelstand")
            {
                waterLevelForm.IsVisible = true;                      //source .IsVisible https://learn.microsoft.com/en-us/answers/questions/1160894/net-maui-how-to-show-or-hide-(-collapsed-)-content (last visit: 30.06.24)

                rainfallForm.IsVisible = false;
                //waterLevelRainfallForm.IsVisible = false;
                //measurementStationForm.IsVisible = false;

            }
            else if (selectedItem == "Niederschlag")
            {
                rainfallForm.IsVisible = true;

                waterLevelForm.IsVisible = false;
                //waterLevelRainfallForm.IsVisible = false;
                //measurementStationForm.IsVisible = false;
            }
            else if (selectedItem == "Pegelstand und Niederschlag")
            {
                //waterLevelRainfallForm.IsVisible = true;

                waterLevelForm.IsVisible = false;
                rainfallForm.IsVisible = false;
                //measurementStationForm.IsVisible = false;
            }
            else if (selectedItem == "Messstation")
            {
                // measurementStationForm.IsVisible = true;

                waterLevelForm.IsVisible = false;
                rainfallForm.IsVisible = false;
                // waterLevelRainfallForm.IsVisible = false;
            }
            else
            {
                waterLevelForm.IsVisible = false;
                rainfallForm.IsVisible = false;
                //waterLevelRainfallForm.IsVisible = false;
                //measurementStationForm.IsVisible = false;

            }
        }
    }


    public void OnAddClicked(object sender, EventArgs e)
    {
        /*
        
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;
        string selectedItem = (string)picker.ItemsSource[selectedIndex];


        if (selectedItem == "Pegelstand")
        {

            ListHistory.Add(new InputintoHistory
            {
                measurementStationName = inputMeasurementStationNameW.Text,
                lon = Convert.ToDouble(inputLonW.Text),
                lat = Convert.ToDouble(inputLatW.Text),
                date = Convert.ToInt32(inputDateW.Text),
                information = inputInformationW.Text,
                measurementData = Convert.ToDouble(inputMeasurementDataW.Text)
            });

        }
        else if (selectedItem == "Niderschlag")
        {
            ListRainfallStation.Add(new InputRainfallData
            {
                measurementStationName = inputMeasurementStationNameR.Text,
                lon = Convert.ToDouble(inputLonR.Text),
                lat = Convert.ToDouble(inputLatR.Text),
                date = Convert.ToInt32(inputDateR.Text),
                information = inputInformationR.Text,
                measurementData = Convert.ToDouble(inputMeasurementDataR.Text)
            });

        }


















































































































































































































        */
        ListWaterlevelStation.Add(new InputWaterlevelData
        {
            measurementStationName = inputMeasurementStationNameW.Text,
            lon = Convert.ToDouble(inputLonW.Text),
            lat = Convert.ToDouble(inputLatW.Text),
            date = Convert.ToInt32(inputDateW.Text),
            information = inputInformationW.Text,
            measurementData = Convert.ToDouble(inputMeasurementDataW.Text)
        });

        ListRainfallStation.Add(new InputRainfallData
        {
            measurementStationName = inputMeasurementStationNameR.Text,
            lon = Convert.ToDouble(inputLonR.Text),
            lat = Convert.ToDouble(inputLatR.Text),
            date = Convert.ToInt32(inputDateR.Text),
            information = inputInformationR.Text,
            measurementData = Convert.ToDouble(inputMeasurementDataR.Text)
        });



        // mit der Länge der Listen arbeiten zum aktualisieren ?
    }


}

