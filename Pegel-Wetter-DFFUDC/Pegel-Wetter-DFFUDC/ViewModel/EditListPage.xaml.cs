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
    private RainfallModel selectedItem;
    ObservableCollection<ModelInputintoHistory> listHistory;

    public static string premeasurementStationName;

    public static string predatatype;

    public static double prelon;

    public static double prelat;

    public static DateTime predate;

  //  public static string preinformation;

  //  public static double premeasurementData;

    public EditListPage(RainfallModel selectedItem, ObservableCollection<ModelInputintoHistory> listHistory)
    {
        InitializeComponent();
        this.selectedItem = selectedItem;
        this.listHistory = listHistory;
        BindingContext = selectedItem;
 
        premeasurementStationName = selectedItem.StationName;

        prelon = selectedItem.Longitude;

        prelat = selectedItem.Latitude;

    }


    private void EditButtonClicked(object sender, EventArgs e)
    {
        listHistory.Add(new ModelInputintoHistory
        {
            edittype = "edited",
            datatype = predatatype,
            measurementStationName = premeasurementStationName,
            lon = prelon,
            lat = prelat,
            date = predate,

            newmeasurementStationName = measurementStationNameEntry.Text,
            newlon = double.Parse(lonEntry.Text),
            newlat = double.Parse(latEntry.Text),
            newdate = DateTime.Parse(dateEntry.Text),
            newinformation = informationEntry.Text,
            newmeasurementData = double.Parse(measurementDataEntry.Text)


        });

        selectedItem.StationName = measurementStationNameEntry.Text;
        selectedItem.Longitude = double.Parse(lonEntry.Text);
        selectedItem.Latitude = double.Parse(latEntry.Text);
        selectedItem.FromDate = DateTime.Parse(dateEntry.Text);

        Navigation.PopAsync();
    }
}