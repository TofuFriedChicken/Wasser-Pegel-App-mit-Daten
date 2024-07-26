using Pegel_Wetter_DFFUDC.Model;

namespace Pegel_Wetter_DFFUDC.ViewModel;

public partial class DetailPage : ContentPage
{
	public DetailPage(InputRainfallData selectedItem)
	{
		InitializeComponent();
        BindingContext = selectedItem; 

    }
}