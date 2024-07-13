
namespace Pegel_Wetter_DFFUDC;

  public partial class InputFormMeasurementData : ContentPage
  {
    public InputFormMeasurementData()
    {
      InitializeComponent();

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
    

    public async void OnAddClicked(object sender, EventArgs e) 
    {

      //check for valid input

      if (!Double.TryParse(inputLonW.Text, out _))    //source TryParse(): https://learn.microsoft.com/de-de/dotnet/api/system.double.tryparse?view=net-8.0  (last visit: 13.07.2024)
      {
        inputLonW.TextColor = Colors.Red;
      }

      if (!Double.TryParse(inputLatW.Text, out _))    
      {
        inputLatW.TextColor = Colors.Red;
      }

      if (!Double.TryParse(inputMeasurementDataW.Text, out _))    
      {
        inputMeasurementDataW.TextColor = Colors.Red;
      }



      if (!Double.TryParse(inputLonR.Text, out _))    
      {
        inputLonR.TextColor = Colors.Red;
      }

      if (!Double.TryParse(inputLatR.Text, out _))    
      {
        inputLatR.TextColor = Colors.Red;
      }

      if (!Double.TryParse(inputMeasurementDataR.Text, out _))    
      {
        inputMeasurementDataR.TextColor = Colors.Red;
      }
    
      if (waterLevelForm.IsVisible && Double.TryParse(inputLonW.Text, out _) && Double.TryParse(inputLatW.Text, out _) && Double.TryParse(inputMeasurementDataW.Text, out _))
      {
        InputWaterlevelData inputWaterlevelData = new InputWaterlevelData
      {
        measurementStationName = inputMeasurementStationNameW.Text,
        lon = Convert.ToDouble(inputLonW.Text),
        lat = Convert.ToDouble(inputLatW.Text),
        //date = ,
        information = inputInformationW.Text,
        measurementData = Convert.ToDouble(inputMeasurementDataW.Text)
      };

      string measurementStationName = inputWaterlevelData.measurementStationName;
    
      test.Text = measurementStationName;

      }
      else if (rainfallForm.IsVisible && Double.TryParse(inputLonR.Text, out _) && Double.TryParse(inputLatR.Text, out _) && Double.TryParse(inputMeasurementDataR.Text, out _))
      {
        InputRainfallData inputRainfallData = new InputRainfallData
      {
        measurementStationName = inputMeasurementStationNameR.Text,
        lon = Convert.ToDouble(inputLonR.Text),
        lat = Convert.ToDouble(inputLatR.Text),
        //date = ,
        information = inputInformationR.Text,
        measurementData = Convert.ToDouble(inputMeasurementDataR.Text)
      };
      }
      else 
      {
        await DisplayAlert("Fehler", "Bitte überprüfe deine Eingabe! Elemente fehlen oder sind fehlerhaft. Für Koordinaten und Messdaten sind nur Zahlen, keine Buchstaben, zulässig.", "Eingabe überarbeiten");    //source Display Alert: https://learn.microsoft.com/de-de/dotnet/maui/user-interface/pop-ups?view=net-maui-8.0#display-an-alert  (last visit: 13.07.2024)
      }
      




      

      // var pinW = new Pin
      // {
      //   Location = new Location (inputWaterlevelData.lon, inputWaterlevelData.lat)
      // };

      // var pinR = new Pin
      // {
      //   Location = new Location (inputWaterlevelData.lon, inputWaterlevelData.lat)
      // };


      
    }
    

  }

