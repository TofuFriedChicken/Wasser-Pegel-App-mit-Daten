using Pegel_Wetter_DFFUDC.Model;
using System.Collections.ObjectModel;

namespace Pegel_Wetter_DFFUDC;

public partial class HistoryPage : ContentPage
{

    public ObservableCollection<InputRainfallData> ListRainfallStation { get; set; }

    public ObservableCollection<InputWaterlevelData> ListWaterlevelStation { get; set; }

    public ObservableCollection<ModelInputintoHistory> ListHistory { get; set; }


    public HistoryPage()
	{
        ListRainfallStation = new ObservableCollection<InputRainfallData>
            {
              new InputRainfallData { datatype="rainfall", measurementStationName = "Alice", lon=4, lat=256, date=12, information="6", measurementData=2},
              new InputRainfallData { datatype="rainfall", measurementStationName = "Alice", lon=4, lat=256, date=12, information="6", measurementData=2},
              new InputRainfallData { datatype="rainfall", measurementStationName = "Alice", lon=4, lat=256, date=12, information="6", measurementData=2},
        };



            ListWaterlevelStation = new ObservableCollection<InputWaterlevelData>()
        {
              new InputWaterlevelData {datatype="waterlevel", measurementStationName = "hehe", lon = 3, lat = 123, date = 1123, information = "123", measurementData = 123 },
        };

        ListHistory = new ObservableCollection<ModelInputintoHistory>()
        {
              new ModelInputintoHistory {datatype="waterlevel", measurementStationName = "hehe", lon = 3, lat = 123, date = 1123, information = "123", measurementData = 123 },
        };



        InitializeComponent();

        BindingContext = this;

    }



    private void OnTurnBackClicked(object sender, EventArgs e)
    {

    }

}