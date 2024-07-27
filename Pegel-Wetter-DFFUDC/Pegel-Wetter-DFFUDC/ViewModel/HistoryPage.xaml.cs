using HarfBuzzSharp;
using Pegel_Wetter_DFFUDC.Model;
using Pegel_Wetter_DFFUDC.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Windows.Input;

namespace Pegel_Wetter_DFFUDC;

public partial class HistoryPage : ContentPage
{

    private static readonly Lazy<HistoryPage> lazy = new Lazy<HistoryPage>(() => new HistoryPage());

    public static HistoryPage Instance { get { return lazy.Value; } }

    public ObservableCollection<RainfallModel> ListRainfallStation => DataStore.Instance.ListRainfallStation;

    public ObservableCollection<RainfallModeldummy> ListRainfallStationdummy => DataStore.Instance.ListRainfallStationDummy;

    public ObservableCollection<WaterlevelModeldummy> ListWaterfallStationdummy => DataStore.Instance.ListWaterlevelStationDummy;


    public ObservableCollection<WaterLevelModel.Root> ListWaterlevelStation => DataStore.Instance.ListWaterlevelStation;

    public ObservableCollection<ModelInputintoHistory> ListHistory => DataStore.Instance.ListHistory;

    private List<ClassofHistoryforJumps> ListofListHistoryofEdits { get; set; }

    private List<ClassofMainListforJumps> ListofMainlist { get; set; }

    private List<RainfallModel> RainfallDataset { get; set; }


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
        
        // history list
        if (e.SelectedItem is ModelInputintoHistory selectedItemhistory)
        {
            action = await DisplayActionSheet("ActionSheet: Send to?", "Cancel", null, "Return", "Delete");
            switch (action)
            {
                case "Return":
                    SaveCurrentHistory();
                    SaveCurrentMainlist();
                    HistoryMethodClass historyreturn = new HistoryMethodClass();
                    historyreturn.HistoryReturnElement(ListHistory, ListRainfallStation, selectedItemhistory);
                    // historyreturn.HistoryReturnElement(ModelInputintoHistory.GetSingletonHistoryList().ListHistory, InputRainfallData.GetSingletonRainfall().ListRainfallStation, selectedItemhistory);
                    break;
                case "Delete":
                    bool confirmation = await DisplayAlert("Datensatz löschen", "Willst du diese Daten wirklich löschen? Sie können danach nicht wieder hergestellt werden.", "Löschen", "Abbrechen");    //source display alert: https://learn.microsoft.com/de-de/dotnet/maui/user-interface/pop-ups?view=net-maui-8.0#display-an-alert (last visist: 14.07.24)
                    if (confirmation)
                    {
                        ListHistory.Remove(selectedItemhistory);
                    }
                    break;    
                default:
                    break;
            }

        }

        // rainfall data list
        if (e.SelectedItem is RainfallModel selectedItemmainlist)
        {
            action = await DisplayActionSheet("ActionSheet: Send to?", "Cancel", null, "Edit", "Detail", "Delete");

            switch (action)
            {
                case "Edit":
                    SaveCurrentHistory();
                    SaveCurrentMainlist();
                   // History.Add(selectedItemmainlist); // Speichern der alten Werte in die History
                    HistoryMethodClass listedit = new HistoryMethodClass();
                    await Navigation.PushAsync(new EditListPage(selectedItemmainlist, ListHistory));
                    break;
                case "Detail":
                    HistoryMethodClass listdetail = new HistoryMethodClass();
                    await Navigation.PushAsync(new DetailPage(selectedItemmainlist));
                    break;
                case "Delete":
                    bool confirmation = await DisplayAlert("Datensatz löschen", "Willst du diese Daten wirklich löschen? Sie können danach nicht wieder hergestellt werden.", "Löschen", "Abbrechen");    //source display alert: https://learn.microsoft.com/de-de/dotnet/maui/user-interface/pop-ups?view=net-maui-8.0#display-an-alert (last visist: 14.07.24)
                    if (confirmation)
                    {
                        ListRainfallStation.Remove(selectedItemmainlist);
                    }
                    break;     
                default:
                    break;
            }

        }

        // waterlevel data list
        if (e.SelectedItem is WaterLevelModel.Root selectedItemWaterLevelList)
        {
            action = await DisplayActionSheet("ActionSheet: Send to?", "Cancel", null, "Edit", "Detail", "Delete");

            switch (action)
            {
                // case "Edit":
                //     SaveCurrentHistory();
                //     SaveCurrentMainlist();
                //    // History.Add(selectedItemmainlist); // Speichern der alten Werte in die History
                //     HistoryMethodClass listedit = new HistoryMethodClass();
                //     await Navigation.PushAsync(new EditListPage(selectedItemWaterLevelList, ListHistory));
                //     break;
                // case "Detail":
                //     HistoryMethodClass listdetail = new HistoryMethodClass();
                //     await Navigation.PushAsync(new DetailPage(selectedItemWaterLevelList));
                //     break;
                case "Delete":
                    bool confirmation = await DisplayAlert("Datensatz löschen", "Willst du diese Daten wirklich löschen? Sie können danach nicht wieder hergestellt werden.", "Löschen", "Abbrechen");    //source display alert: https://learn.microsoft.com/de-de/dotnet/maui/user-interface/pop-ups?view=net-maui-8.0#display-an-alert (last visist: 14.07.24)
                    if (confirmation)
                    {
                        ListWaterlevelStation.Remove(selectedItemWaterLevelList);
                    }
                        
                    
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
