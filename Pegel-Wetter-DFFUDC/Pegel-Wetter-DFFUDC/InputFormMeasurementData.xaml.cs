
using System.Collections.ObjectModel;

namespace Pegel_Wetter_DFFUDC;

public partial class InputFormMeasurementData : ContentPage
{

    public ObservableCollection<InputRainfallData> ListRainfallStation { get; set; }

    public ObservableCollection<InputWaterlevelData> ListWaterlevelStation { get; set; }



    public InputFormMeasurementData()
    {
        InitializeComponent();

        ListRainfallStation = new ObservableCollection<InputRainfallData>
            {
              new InputRainfallData { measurementStationName = "Alice", lon=4, lat=256, date=12, information="6", measurementData=2},
              new InputRainfallData { measurementStationName = "Alice", lon=4, lat=256, date=12, information="6", measurementData=2},
              new InputRainfallData { measurementStationName = "Alice", lon=4, lat=256, date=12, information="6", measurementData=2},

        };


        ListWaterlevelStation = new ObservableCollection<InputWaterlevelData>()
        {
        new InputWaterlevelData { measurementStationName = "Alice", lon = 4, lat = 256, date = 12, information = "6", measurementData = 2 },
        };

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
        InputWaterlevelData inputWaterlevelData = new InputWaterlevelData
        {
            measurementStationName = inputMeasurementStationNameW.Text,
            lon = Convert.ToDouble(inputLonW.Text),
            lat = Convert.ToDouble(inputLatW.Text),
            date = Convert.ToInt32(inputDateW.Text),
            information = inputInformationW.Text,
            measurementData = Convert.ToDouble(inputMeasurementDataW.Text)
        };

        InputRainfallData inputRainfallData = new InputRainfallData
        {
            measurementStationName = inputMeasurementStationNameR.Text,
            lon = Convert.ToDouble(inputLonR.Text),
            lat = Convert.ToDouble(inputLatR.Text),
            date = Convert.ToInt32(inputDateR.Text),
            information = inputInformationR.Text,
            measurementData = Convert.ToDouble(inputMeasurementDataR.Text)
        };



        // var pinW = new Pin
        // {
        //   Location = new Location (inputWaterlevelData.lon, inputWaterlevelData.lat)
        // };

        // var pinR = new Pin
        // {
        //   Location = new Location (inputWaterlevelData.lon, inputWaterlevelData.lat)
        // };


        string measurementStationName = inputWaterlevelData.measurementStationName;

        test.Text = measurementStationName;



    }


}

