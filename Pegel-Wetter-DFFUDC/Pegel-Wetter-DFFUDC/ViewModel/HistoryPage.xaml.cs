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

    public ObservableCollection<InputRainfallData> ListRainfallStation => DataStore.Instance.ListRainfallStation;

    public ObservableCollection<ModelInputintoHistory> ListHistory => DataStore.Instance.ListHistory;

    private List<ClassofHistoryforJumps> ListofListHistoryofEdits { get; set; }

    private List<ClassofMainListforJumps> ListofMainlist { get; set; }

    private List<RainfallViewModel> RainfallDataset { get; set; }


    public HistoryPage()
    {

        ListofListHistoryofEdits = new List<ClassofHistoryforJumps>();

        ListofMainlist = new List<ClassofMainListforJumps>();


        InitializeComponent();

        BindingContext = this;

    }


    public void InitializeData()
    {
        List<ModelInputintoHistory> loadmainlist = ListHistory.ToList();
        List<InputRainfallData> loadhistorylist = ListRainfallStation.ToList();
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
                    SaveCurrentHistory();
                    SaveCurrentMainlist();
                    HistoryMethodClass historyreturn = new HistoryMethodClass();
                    historyreturn.HistoryReturnElement(ListHistory, ListRainfallStation, selectedItemhistory);
                    // historyreturn.HistoryReturnElement(ModelInputintoHistory.GetSingletonHistoryList().ListHistory, InputRainfallData.GetSingletonRainfall().ListRainfallStation, selectedItemhistory);
                    break;
                default:
                    break;
            }

        }

        if (e.SelectedItem is InputRainfallData selectedItemmainlist)
        {
            action = await DisplayActionSheet("ActionSheet: Send to?", "Cancel", null, "Edit", "Detail");

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

    public ICommand DeleteCommand => new Command<ModelInputintoHistory>(RemoveChemical);


    void RemoveChemical(ModelInputintoHistory listhistory)
    {
        if (ListHistory.Contains(listhistory))
        {
            ListHistory.Remove(listhistory);
        }
    }


}
