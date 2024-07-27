
using Pegel_Wetter_DFFUDC.Model;
using Microsoft.Maui.Maps;
using Microsoft.Maui.Controls.Maps;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace Pegel_Wetter_DFFUDC;

public partial class InputFormMeasurementData : ContentPage
{

  //other App pages
  MainPage mainPage = new MainPage();
  HistoryPage historyPage = new HistoryPage();


  public InputFormMeasurementData()
  {
    InitializeComponent();
  }


  // Picker for different input forms
  string dataType = "";
  private void OnPickerSelectedIndexChanged(object sender, EventArgs e)     //source eventhandler https://learn.microsoft.com/de-de/dotnet/maui/user-interface/controls/picker?view=net-maui-8.0 (last visit: 30.06.24)
  {
    var picker = (Picker)sender;
    int selectedIndex = picker.SelectedIndex;

    if (selectedIndex != -1)            //picker has default value -1 (= no item selected)
    {
      string selectedItem = (string)picker.ItemsSource[selectedIndex];

      if (selectedItem == "Pegelstand")
      {
        waterLevelForm.IsVisible = true;                      //source .IsVisible https://learn.microsoft.com/en-us/answers/questions/1160894/net-maui-how-to-show-or-hide-(-collapsed-)-content (last visit: 30.06.24)
        rainfallForm.IsVisible = false;
        dataType = "waterlevel";
      }
      else if (selectedItem == "Niederschlag")
      {
        rainfallForm.IsVisible = true;
        waterLevelForm.IsVisible = false; 
        dataType = "rainfall"; 
      }
      else
      {
        waterLevelForm.IsVisible = false;
        rainfallForm.IsVisible = false;
      }
    }

  }


  // Date Picker 
  DateTime measurementDataDate = DateTime.Today;
  public async void OnDateClicked(object sender, DateChangedEventArgs e)      //method source: https://learn.microsoft.com/de-de/dotnet/maui/user-interface/controls/datepicker?view=net-maui-8.0#localize-a-datepicker-on-windows, https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.datechangedeventargs.-ctor?view=net-maui-8.0#microsoft-maui-controls-datechangedeventargs-ctor(system-datetime-system-datetime) and help of ChatGPT (last visit websites: 15.07.24)
  {
    DateTime selectedDate = e.NewDate;
    DateTime today = DateTime.Today;

    if (selectedDate > today)                     //check date selection
    {
      datePickerW.MaximumDate = DateTime.Today;                   //source date picker: https://learn.microsoft.com/de-de/dotnet/maui/user-interface/controls/datepicker?view=net-maui-8.0#localize-a-datepicker-on-windows  (last visit: 15.07.24)
      datePickerR.MaximumDate = DateTime.Today;
      await DisplayAlert("Achtung!", "Dein gewähltes Datum liegt in der Zukunft! Bitte wähle ein anderes Datum, andernfalls wird das aktuelle Datum verwendet.", "Anderes Datum wählen");
    }
    else 
    {
      measurementDataDate = e.NewDate;
    }   
  }


  // Add Pin for measurement station
  public void AddPinToMap(Map map)
  {
    //Waterlevel Pin
    if (waterLevelForm.IsVisible && !string.IsNullOrWhiteSpace(inputLonW.Text) && !string.IsNullOrWhiteSpace(inputLatW.Text))
    {
      string measurementStationName = inputMeasurementStationNameW.Text;
      double lat = Convert.ToDouble(inputLatW.Text);
      double lon = Convert.ToDouble(inputLonW.Text);
      
      var pinW = new Pin
      {
        Label = measurementStationName,
        Location = new Location (lat, lon)
      };

      map.Pins.Add(pinW);
    }

    //Rainfall Pin
    if (rainfallForm.IsVisible && !string.IsNullOrWhiteSpace(inputLonR.Text) && !string.IsNullOrWhiteSpace(inputLatR.Text))
    {
      string measurementStationName = inputMeasurementStationNameR.Text;
      double lat = Convert.ToDouble(inputLatR.Text); 
      double lon = Convert.ToDouble(inputLonR.Text);

      var pinR = new Pin
      {
        Label = measurementStationName,
        Location = new Location (lat, lon)
      };

      map.Pins.Add(pinR);
    }
    
  }


  // get data from input fields
  public async void OnAddClicked(object sender, EventArgs e)
  {
    //check for valid input
    if (waterLevelForm.IsVisible && !string.IsNullOrWhiteSpace(inputMeasurementStationNameW.Text) && !string.IsNullOrWhiteSpace(inputLonW.Text) && !string.IsNullOrWhiteSpace(inputLatW.Text) && !string.IsNullOrWhiteSpace(inputInformationW.Text) && !string.IsNullOrWhiteSpace(inputMeasurementDataW.Text))
    {

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


      //get water level data
      if (Double.TryParse(inputLonW.Text, out _) && Double.TryParse(inputLatW.Text, out _) && Double.TryParse(inputMeasurementDataW.Text, out _))
      {
        InputWaterlevelData inputWaterlevelData = new InputWaterlevelData
        {
          datatype = dataType, 
          measurementStationName = inputMeasurementStationNameW.Text,
          lon = Convert.ToDouble(inputLonW.Text),
          lat = Convert.ToDouble(inputLatW.Text),
          date = measurementDataDate,
          information = inputInformationW.Text,
          measurementData = Convert.ToDouble(inputMeasurementDataW.Text)
        };

        //add water level data to map and list 
        mainPage.Appearing += (s, args) => { AddPinToMap(mainPage.Map); };        //source https://learn.microsoft.com/de-de/dotnet/api/microsoft.maui.controls.baseshellitem.appearing#microsoft-maui-controls-baseshellitem-appearing + help of ChatGPT (last visit: 27.07.24)
        historyPage.ListWaterlevelStation.Add(inputWaterlevelData);

			  bool alert = await DisplayAlert("Daten erfolgreich hinzugefügt.", "Wo möchtest du dir deine Daten anschauen?", "Messstation auf Karte anzeigen", "Daten in Liste anzeigen");

        //select next step
			  if (alert)
			  {
				  await Navigation.PushAsync(mainPage); 
			  }  
        else
        {
          await Navigation.PushAsync(historyPage);
        }
              
      }
      else
      {
        await DisplayAlert("Fehler", "Bitte überprüfe deine Eingabe! Für Koordinaten und Messdaten sind nur Zahlen, keine Buchstaben, zulässig.", "Eingabe überarbeiten");    //source Display Alert: https://learn.microsoft.com/de-de/dotnet/maui/user-interface/pop-ups?view=net-maui-8.0#display-an-alert  (last visit: 13.07.2024)
      }

    }
    else if (rainfallForm.IsVisible && !string.IsNullOrWhiteSpace(inputMeasurementStationNameR.Text) && !string.IsNullOrWhiteSpace(inputLonR.Text) && !string.IsNullOrWhiteSpace(inputLatR.Text) && !string.IsNullOrWhiteSpace(inputInformationR.Text) && !string.IsNullOrWhiteSpace(inputMeasurementDataR.Text))
    {
      //check for valid input
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

      //get rainfall data
      if (Double.TryParse(inputLonR.Text, out _) && Double.TryParse(inputLatR.Text, out _) && Double.TryParse(inputMeasurementDataR.Text, out _))
      {
        InputRainfallData inputRainfallData = new InputRainfallData
        {
          datatype = dataType,
          measurementStationName = inputMeasurementStationNameR.Text,
          lon = Convert.ToDouble(inputLonR.Text),
          lat = Convert.ToDouble(inputLatR.Text),
          date = measurementDataDate,
          information = inputInformationR.Text,
          measurementData = Convert.ToDouble(inputMeasurementDataR.Text)
        };

        //add rainfall data to map and list
        mainPage.Appearing += (s, args) => { AddPinToMap(mainPage.Map); };        //source https://learn.microsoft.com/de-de/dotnet/api/microsoft.maui.controls.baseshellitem.appearing#microsoft-maui-controls-baseshellitem-appearing + help of ChatGPT (last visit: 27.07.24)
        historyPage.ListRainfallStation.Add(inputRainfallData);
        
			  bool alert = await DisplayAlert("Daten erfolgreich hinzugefügt.", "Wo möchtest du dir deine Daten anschauen?", "Messstation auf Karte anzeigen", "Daten in Liste anzeigen");

        //select next step
			  if (alert)
			  {
				  await Navigation.PushAsync(mainPage); 
			  }  
        else
        {
          await Navigation.PushAsync(historyPage);
        }
        
      }
      else
      {
       await DisplayAlert("Fehler", "Bitte überprüfe deine Eingabe! Für Koordinaten und Messdaten sind nur Zahlen, keine Buchstaben, zulässig.", "Eingabe überarbeiten");    
      }
    }
    else
    {
      await DisplayAlert("Fehler", "Bitte trage in alle Felder Daten ein.", "Eingabe überarbeiten");    
    }

  }


}

