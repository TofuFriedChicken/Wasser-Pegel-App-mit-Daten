using Pegel_Wetter_DFFUDC.Model;

namespace Pegel_Wetter_DFFUDC.ViewModel;

public partial class DetailPageRain : ContentPage
{
	public DetailPageRain(RainfallModel selectedItemrainparam)
	{
		InitializeComponent();
        BindingContext = selectedItemrainparam; 


    }

}