namespace Pegel_Wetter_DFFUDC
{
  public partial class InputFormWeatherData : ContentPage
  {
    public InputFormWeatherData()
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
          waterLevelRainfallForm.IsVisible = false;
          weatherStationForm.IsVisible = false;

        }
        else if (selectedItem == "Niederschlag")
        {
          rainfallForm.IsVisible = true;

          waterLevelForm.IsVisible = false;
          waterLevelRainfallForm.IsVisible = false;
          weatherStationForm.IsVisible = false;
        }
        else if (selectedItem == "Pegelstand und Niederschlag")
        {
          waterLevelRainfallForm.IsVisible = true;

          waterLevelForm.IsVisible = false;
          rainfallForm.IsVisible = false;
          weatherStationForm.IsVisible = false;
        }
        else if (selectedItem == "Messstation")
        {
          weatherStationForm.IsVisible = true;

          waterLevelForm.IsVisible = false;
          rainfallForm.IsVisible = false;
          waterLevelRainfallForm.IsVisible = false;
        }
        else
        {
          waterLevelForm.IsVisible = false;
          rainfallForm.IsVisible = false;
          waterLevelRainfallForm.IsVisible = false;
          weatherStationForm.IsVisible = false;
          
        }
      }
      
    }


    public void OnAddClicked(object sender, EventArgs e) 
    {
      string weatherStationName = inputWeatherStationName.Text;
      string lon = inputLon.Text;
      string lat = inputLat.Text;
      string date = inputDate.Text;
      string time = inputTime.Text;
      string water = inputWater.Text;
      string waterLevel = inputWaterLevel.Text;

      test.Text = weatherStationName;
    }
    

  }

}