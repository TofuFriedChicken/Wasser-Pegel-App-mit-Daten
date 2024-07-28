using HarfBuzzSharp;
using Pegel_Wetter_DFFUDC.Model;
using Pegel_Wetter_DFFUDC.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Windows.Input;

namespace Pegel_Wetter_DFFUDC;

public partial class HistoryPage : ContentPage
{    public ObservableCollection<RainfallModeldummy> ListRainfallStationdummy => DataStore.Instance.ListRainfallStationDummy;

    public ObservableCollection<WaterlevelModeldummy> ListWaterfallStationdummy => DataStore.Instance.ListWaterlevelStationDummy;

    private static readonly Lazy<HistoryPage> lazy = new Lazy<HistoryPage>(() => new HistoryPage());

    public static HistoryPage Instance { get { return lazy.Value; } }

    public ObservableCollection<RainfallModel> ListRainfallStation => DataStore.Instance.ListRainfallStation;

 //   public ObservableCollection<RainfallModeldummy> ListRainfallStationdummy => DataStore.Instance.ListRainfallStationdummy;

 //   public ObservableCollection<WaterlevelModeldummy> ListWaterfallStationdummy => DataStore.Instance.ListWaterfallStationdummy;


    public ObservableCollection<WaterLevelModel.Root> ListWaterlevelStation => DataStore.Instance.ListWaterlevelStation;

    public ObservableCollection<ModelInputintoHistory> ListHistory => DataStore.Instance.ListHistory;

    private List<ClassofHistoryforJumps> ListofListHistoryofEdits { get; set; }

    private List<ClassofMainListforJumps> ListofMainlist { get; set; }

    private List<RainfallModel> RainfallDataset { get; set; }

    private List<WaterLevelModel.Root> WaterlevelDataset { get; set; }



    public HistoryPage()
    {

        ListofListHistoryofEdits = new List<ClassofHistoryforJumps>();

        ListofMainlist = new List<ClassofMainListforJumps>();
        BindingContext = this;


        InitializeComponent();


    }


    public void InitializeData()
    {
        List<ModelInputintoHistory> loadmainlist = ListHistory.ToList();
        List<RainfallModel> loadhistorylist = ListRainfallStation.ToList();
        List<WaterLevelModel.Root> loadhistorylistwater = ListWaterlevelStation.ToList();

        SaveCurrentMainlist();
        SaveCurrentHistory();
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

        string action;
        
        if (e.SelectedItem is ModelInputintoHistory selectedItemhistory)
        {
            action = await DisplayActionSheet("ActionSheet: Send to?", "Cancel", null, "Return");
            switch (action)
            {
                case "Return":
                    if (selectedItemhistory.datatype == "rainfall")
                    {
                        SaveCurrentHistory();
                        SaveCurrentMainlist();
                        HistoryMethodClass historyreturn = new HistoryMethodClass();
                        historyreturn.HistoryReturnElementrainfall(ListHistory, ListRainfallStation, selectedItemhistory);
                    }
                    else if (selectedItemhistory.datatype == "waterlevel")
                    {
                        SaveCurrentHistory();
                        SaveCurrentMainlist();
                        HistoryMethodClass historyreturn = new HistoryMethodClass();
                        historyreturn.HistoryReturnElementrainfall(ListHistory, ListRainfallStation, selectedItemhistory);
                    }
                    break;
                default:
                    break;
            }

        }

        if (e.SelectedItem is RainfallModel selectedItemrainparam)
        {
            action = await DisplayActionSheet("ActionSheet: Send to?", "Cancel", null, "Edit", "Detail");

            switch (action)
            {
                case "Edit":
                    SaveCurrentHistory();
                    SaveCurrentMainlist();
                   // History.Add(selectedItemmainlist); // Speichern der alten Werte in die History
                    HistoryMethodClass listedit = new HistoryMethodClass();
                    await Navigation.PushAsync(new EditListPage(selectedItemrainparam, ListHistory));
                    break;
                case "Detail":
                    HistoryMethodClass listdetail = new HistoryMethodClass();
                    await Navigation.PushAsync(new DetailPageRain(selectedItemrainparam));
                    break;
                default:
                    break;
            }

        }

        if (e.SelectedItem is WaterLevelModel.Root selectedItemwaterparam)
        {
            action = await DisplayActionSheet("ActionSheet: Send to?", "Cancel", null, "Edit", "Detail");

            switch (action)
            {
                case "Edit":
                    SaveCurrentHistory();
                    SaveCurrentMainlist();
                    // History.Add(selectedItemmainlist); // Speichern der alten Werte in die History
                    HistoryMethodClass listedit = new HistoryMethodClass();
                    await Navigation.PushAsync(new EditListPage(selectedItemwaterparam, ListHistory));
                    break;
                case "Detail":
                    HistoryMethodClass listdetail = new HistoryMethodClass();
                    await Navigation.PushAsync(new DetailPageWater(selectedItemwaterparam));
                    break;
                default:
                    break;
            }

        }

    }

    private void OnRainfallButtonClick(object sender, EventArgs e)
    {
        RainfallListView.IsVisible = true;
        WaterlevelListView.IsVisible = false;
    }

    private void OnWaterlevelButtonClick(object sender, EventArgs e)
    {
        RainfallListView.IsVisible = false;
        WaterlevelListView.IsVisible = true;
    }

    private void OnHistoryFowardClick(object sender, EventArgs e)
    {
        JumpHistory();
    }

    private void OnHistoryBackwardClick(object sender, EventArgs e)
    {
        JumpHistory();
    }
    /*
    public ICommand DeleteCommand => new Command<ModelInputintoHistory>(RemoveListMembers);


    void RemoveListMembers(ModelInputintoHistory listhistory)
    {
        if (ListHistory.Contains(listhistory))
        {
            ListHistory.Remove(listhistory);
        }
    }
    */

}
