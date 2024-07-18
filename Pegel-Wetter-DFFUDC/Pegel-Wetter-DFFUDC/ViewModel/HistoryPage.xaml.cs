using Pegel_Wetter_DFFUDC.Model;
using Pegel_Wetter_DFFUDC.ViewModel;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

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
              new ModelInputintoHistory {edittype="edited", datatype="rainfall", measurementStationName = "hehe1", lon = 3, lat = 123, date = 1123, information = "123", measurementData = 123 },
              new ModelInputintoHistory {edittype="added", datatype="waterlevel", measurementStationName = "hehe2", lon = 3, lat = 123, date = 1123, information = "123", measurementData = 123 },
              new ModelInputintoHistory {edittype="deleted", datatype="waterlevel", measurementStationName = "hehe3", lon = 3, lat = 123, date = 1123, information = "123", measurementData = 123 },


        };



        InitializeComponent();

        BindingContext = this;

    }

    
    async private void OnOptionClicked(object sender, EventArgs e)
    {
        string action = await DisplayActionSheet("ActionSheet: Send to?", "Cancel", null, "Return", "Edit");

        switch (action)
        {
            case "Return":
                HistoryMethodClass historyreturn = new HistoryMethodClass();
                historyreturn.HistoryReturn();
                break;
            case "Edit":
                HistoryMethodClass historylistedit = new HistoryMethodClass();
                historylistedit.ListEdit();
                break;
            default:
                break;
        }
    }

    private void EditButton_Clicked(object sender, EventArgs e)
    {

    }

    private void ReturnButton_Clicked(object sender, EventArgs e)
    {

    }
}