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
    private Model.RainfallViewModeldummy selectedItem;
    ObservableCollection<ModelInputintoHistory> listHistory;
    public static string predatatype;

    public static string premeasurementStationName;

    public static double prelon;

    public static double prelat;

    public static DateTime predate;

    public static string preinformation;

    public static double premeasurementData;

    public EditListPage(Model.RainfallViewModeldummy selectedItem, ObservableCollection<ModelInputintoHistory> listHistory)
    {
        InitializeComponent();
        this.selectedItem = selectedItem;
        this.listHistory = listHistory;
        BindingContext = selectedItem;
        /*
                if ( EditButtonClicked == true)
                {
                    listHistory.Add(new ModelInputintoHistory
                    {
                        edittype = "edited",
                        datatype = selectedItem.datatype,
                        measurementStationName = selectedItem.measurementStationName,
                        lon = selectedItem.lon,
                        lat = selectedItem.lat,
                        date = selectedItem.date,
                        information = selectedItem.information,
                        measurementData = selectedItem.measurementData,

                        newmeasurementStationName = measurementStationNameEntry.Text,
                        newlon = double.Parse(lonEntry.Text),
                        newlat = double.Parse(latEntry.Text),
                        newdate = DateTime.Parse(dateEntry.Text),
                        newinformation = informationEntry.Text,
                        newmeasurementData = double.Parse(measurementDataEntry.Text)

                    });
                }
        
        */
        // elemente sollen dvor schon gespeichert werden

        predatatype = selectedItem.datatype;

        premeasurementStationName = selectedItem.measurementStationName;

        prelon = selectedItem.lon;

        prelat = selectedItem.lat;

        predate = selectedItem.date;

        preinformation = selectedItem.information;

        premeasurementData = selectedItem.measurementData;

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
            information = preinformation,
            measurementData = premeasurementData,

            newmeasurementStationName = measurementStationNameEntry.Text,
            newlon = double.Parse(lonEntry.Text),
            newlat = double.Parse(latEntry.Text),
            newdate = DateTime.Parse(dateEntry.Text),
            newinformation = informationEntry.Text,
            newmeasurementData = double.Parse(measurementDataEntry.Text)


        });

        /*
        HistoryPage.Instance.ListHistory.Add(new ModelInputintoHistory
        {
            edittype = "edited",
            datatype = predatatype,
            measurementStationName = premeasurementStationName,
            lon = prelon,
            lat = prelat,
            date = predate,
            information = preinformation,
            measurementData = premeasurementData,

            newmeasurementStationName = measurementStationNameEntry.Text,
            newlon = double.Parse(lonEntry.Text),
            newlat = double.Parse(latEntry.Text),
            newdate = DateTime.Parse(dateEntry.Text),
            newinformation = informationEntry.Text,
            newmeasurementData = double.Parse(measurementDataEntry.Text)
        });
        */

        selectedItem.measurementStationName = measurementStationNameEntry.Text;
        selectedItem.lon = double.Parse(lonEntry.Text);
        selectedItem.lat = double.Parse(latEntry.Text);
        selectedItem.date = DateTime.Parse(dateEntry.Text);
        selectedItem.information = informationEntry.Text;
        selectedItem.measurementData = double.Parse(measurementDataEntry.Text);


        Navigation.PopAsync();
    }
}