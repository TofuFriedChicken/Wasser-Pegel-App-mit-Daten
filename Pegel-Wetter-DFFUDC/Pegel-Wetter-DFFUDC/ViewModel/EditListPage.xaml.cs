using Pegel_Wetter_DFFUDC.Model;
using System.Collections.ObjectModel;

namespace Pegel_Wetter_DFFUDC.ViewModel;

public partial class EditListPage : ContentPage
{
    private InputRainfallData selectedItem;
    ObservableCollection<ModelInputintoHistory> listHistory;

    public EditListPage(InputRainfallData selectedItem, ObservableCollection<ModelInputintoHistory> listHistory)
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


    }


    private void EditButtonClicked(object sender, EventArgs e)
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

        selectedItem.measurementStationName = measurementStationNameEntry.Text;
        selectedItem.lon = double.Parse(lonEntry.Text);
        selectedItem.lat = double.Parse(latEntry.Text);
        selectedItem.date = DateTime.Parse(dateEntry.Text);
        selectedItem.information = informationEntry.Text;
        selectedItem.measurementData = double.Parse(measurementDataEntry.Text);


        Navigation.PopAsync();
    }
}