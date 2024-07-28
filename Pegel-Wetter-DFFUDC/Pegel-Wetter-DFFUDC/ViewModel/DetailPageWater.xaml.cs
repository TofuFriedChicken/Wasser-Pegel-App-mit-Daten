using Pegel_Wetter_DFFUDC.Model;


namespace Pegel_Wetter_DFFUDC.ViewModel;

public partial class DetailPageWater : ContentPage
{
    public DetailPageWater(WaterLevelModel.Root selectedItemwaterparam)
    {
        InitializeComponent();
        BindingContext = selectedItemwaterparam;
    }
}