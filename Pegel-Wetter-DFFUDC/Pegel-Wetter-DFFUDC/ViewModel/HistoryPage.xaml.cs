using Pegel_Wetter_DFFUDC.Model;
using Pegel_Wetter_DFFUDC.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace Pegel_Wetter_DFFUDC;

public partial class HistoryPage : ContentPage
{

    public ObservableCollection<InputRainfallData> ListRainfallStation { get; set; }

    public ObservableCollection<InputWaterlevelData> ListWaterlevelStation { get; set; }

    public ObservableCollection<ModelInputintoHistory> ListHistory { get; set; }

    private List<ClassofHistoryforJumps> ListofListHistoryofEdits { get; set; }

    private List<ClassofMainListforJumps> ListofMainlist { get; set; }

    private static readonly Lazy<ModelInputintoHistory> lazy = new Lazy<ModelInputintoHistory>(() => new ModelInputintoHistory());

    public static ModelInputintoHistory Instance { get { return lazy.Value; } }

    public HistoryPage()
    {
        ListRainfallStation = new ObservableCollection<InputRainfallData>
            {
              new InputRainfallData { datatype="rainfall", measurementStationName = "Alice", lon=4, lat=256, information="6", measurementData=2},
              new InputRainfallData { datatype="rainfall", measurementStationName = "Alice", lon=4, lat=256, information="6", measurementData=2},
              new InputRainfallData { datatype="rainfall", measurementStationName = "Alice", lon=4, lat=256, information="6", measurementData=2},
              new InputRainfallData { datatype="waterlevel", measurementStationName = "hehe2", lon = 3, lat = 123, information = "123", measurementData = 123 },
        };

        ListWaterlevelStation = new ObservableCollection<InputWaterlevelData>()
        {
              new InputWaterlevelData {datatype="waterlevel", measurementStationName = "hehe", lon = 3, lat = 123, information = "123", measurementData = 123 },
        };

        ListHistory = new ObservableCollection<ModelInputintoHistory>()
        {
                          new ModelInputintoHistory {edittype="deleted", datatype="waterlevel", measurementStationName = "hehe3", lon = 3, lat = 123, information = "123", measurementData = 123 },

              new ModelInputintoHistory {edittype="edited", datatype="rainfall", measurementStationName = "hehe1", lon = 3, lat = 123, information = "123", measurementData = 123 },
              new ModelInputintoHistory {edittype="added", datatype="waterlevel", measurementStationName = "hehe2", lon = 3, lat = 123, information = "123", measurementData = 123 },
              new ModelInputintoHistory {edittype="deleted", datatype="waterlevel", measurementStationName = "hehe3", lon = 3, lat = 123, information = "123", measurementData = 123 },
              new ModelInputintoHistory {edittype="edited", datatype="rainfall", measurementStationName = "hehe1", lon = 3, lat = 123, information = "123", measurementData = 123 },
              new ModelInputintoHistory {edittype="added", datatype="waterlevel", measurementStationName = "hehe2", lon = 3, lat = 123, information = "123", measurementData = 123 },

        };


        ListofListHistoryofEdits = new List<ClassofHistoryforJumps>();

        ListofMainlist = new List<ClassofMainListforJumps>();

        SaveCurrentMainlist();

        SaveCurrentHistory();

        InitializeComponent();

        BindingContext = this;

    }


    public void SaveCurrentHistory()
    {
        ListofListHistoryofEdits.Add(new ClassofHistoryforJumps(ListHistory));
    }

    public void SaveCurrentMainlist()
    {
        ListofMainlist.Add(new ClassofMainListforJumps(ListRainfallStation));
    }

    private void JumpHistory()
    {   
        if (ListofListHistoryofEdits.Count > 0)
        {
            var Historylistlastscreenshot = ListofListHistoryofEdits.Last();
            ListHistory.Clear();
            foreach (var item in Historylistlastscreenshot.ListHistoryScreenshot)
            {
                ListHistory.Add(item);
            }
            ListofListHistoryofEdits.Remove(Historylistlastscreenshot);
        }
        else { }

        if (ListofMainlist.Count > 0)
        {
            var Mainlistlastscreenshot = ListofMainlist.Last();
            ListRainfallStation.Clear();
            foreach (var item in Mainlistlastscreenshot.MainlistScreenshot)
            {
                ListRainfallStation.Add(item);
            }
            ListofMainlist.Remove(Mainlistlastscreenshot);
        }
        else { }
    }


    async private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

        string action = await DisplayActionSheet("ActionSheet: Send to?", "Cancel", null, "Return", "Edit");

        if (e.SelectedItem is ModelInputintoHistory selectedItem)
        {
            switch (action)
            {
                case "Return":
                    SaveCurrentHistory();
                    SaveCurrentMainlist();
                    HistoryMethodClass historyreturn = new HistoryMethodClass();
                    historyreturn.HistoryReturnElement(ListHistory, ListRainfallStation, selectedItem);
                    break;
                case "Edit":
                    SaveCurrentHistory();
                    SaveCurrentMainlist();
                    HistoryMethodClass historylistedit = new HistoryMethodClass();
                    // historylistedit.ListEdit(selectedItem.edittype);
                    break;
                default:
                    break;
            }

        }


    }

    private void OnHistoryFowardClick(object sender, EventArgs e)
    {
        JumpHistory();
    }

    private void OnHistoryBackwardClick(object sender, EventArgs e)
    {
        JumpHistory();
    }

}