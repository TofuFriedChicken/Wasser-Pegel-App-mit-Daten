using HarfBuzzSharp;
using Microsoft.Maui.Controls;
using Microsoft.VisualBasic;
using Pegel_Wetter_DFFUDC.Model;
using System.Collections.ObjectModel;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations;

namespace Pegel_Wetter_DFFUDC.ViewModel;

public partial class EditListPage : ContentPage
{
    private RainfallModel RainselectedItem;
    private WaterLevelModel.Root WaterselectedItem;

    ObservableCollection<ModelInputintoHistory> listHistory;

    public static string premeasurementStationName;

    public static string predatatype;

    public static double prelon;

    public static double prelat;

    public static DateTime predate;

  //  public static string preinformation;

  //  public static double premeasurementData;

    public EditListPage(RainfallModel selectedItemrainfall, ObservableCollection<ModelInputintoHistory> listHistory)
    {
        InitializeComponent();
        this.RainselectedItem = selectedItemrainfall;
        this.listHistory = listHistory;
        BindingContext = selectedItemrainfall;
 
        premeasurementStationName = selectedItemrainfall.StationName;

        prelon = selectedItemrainfall.Longitude;

        prelat = selectedItemrainfall.Latitude;

    }

    public EditListPage(WaterLevelModel.Root selectedItemwaterlevel, ObservableCollection<ModelInputintoHistory> listHistory)
    {
        InitializeComponent();
        this.WaterselectedItem = selectedItemwaterlevel;
        this.listHistory = listHistory;
        BindingContext = selectedItemwaterlevel;

        premeasurementStationName = selectedItemwaterlevel.longname;

        prelon = selectedItemwaterlevel.longitude;

        prelat = selectedItemwaterlevel.latitude;

    }

    DateTime measurementDataDate = DateTime.Today;
    public async void OnDateEditClicked(object sender, DateChangedEventArgs e)
    {
        DateTime selectedDate = e.NewDate;
        DateTime today = DateTime.Today;

        if (selectedDate > today)
        {
            dateEntry.MaximumDate = DateTime.Today;
            await DisplayAlert("Achtung!", "Dein gewähltes Datum liegt in der Zukunft! Bitte wähle ein anderes Datum, andernfalls wird das aktuelle Datum verwendet.", "Anderes Datum wählen");
        }
        else
        {
            measurementDataDate = e.NewDate;
        }

    }

    private void EditButtonClicked(object sender, EventArgs e)
    {
        if (this.RainselectedItem!=null)
        {
            listHistory.Add(new ModelInputintoHistory
            {
                edittype = "edited",
                datatype = "rainfall",
                measurementStationName = premeasurementStationName,
                lon = prelon,
                lat = prelat,
                date = predate,

                newmeasurementStationName = measurementStationNameEntry.Text,
                newlon = double.Parse(lonEntry.Text),
                newlat = double.Parse(latEntry.Text),
                newdate = measurementDataDate,
                newinformation = informationEntry.Text,
                newmeasurementData = double.Parse(measurementDataEntry.Text)


            });

            RainselectedItem.StationName = measurementStationNameEntry.Text;
            RainselectedItem.Longitude = double.Parse(lonEntry.Text);
            RainselectedItem.Latitude = double.Parse(latEntry.Text);
            RainselectedItem.FromDate = measurementDataDate;
        }
        else if( this.WaterselectedItem != null)
            listHistory.Add(new ModelInputintoHistory
            {
                edittype = "edited",
                datatype = "waterlevel",
                measurementStationName = premeasurementStationName,
                lon = prelon,
                lat = prelat,
                date = predate,

                newmeasurementStationName = measurementStationNameEntry.Text,
                newlon = double.Parse(lonEntry.Text),
                newlat = double.Parse(latEntry.Text),
                newdate = measurementDataDate,
                newinformation = informationEntry.Text,
                newmeasurementData = double.Parse(measurementDataEntry.Text)


            });

            WaterselectedItem.longname = measurementStationNameEntry.Text;
            WaterselectedItem.longitude = double.Parse(lonEntry.Text);
            WaterselectedItem.latitude = double.Parse(latEntry.Text);
        
        

        Navigation.PopAsync();
    }
}