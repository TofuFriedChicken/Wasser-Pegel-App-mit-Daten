using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Microsoft.Maui.ApplicationModel;
using System.Net.NetworkInformation;


namespace Pegel_Wetter_DFFUDC
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

        }

        private async void feature4_Clicked(object sender, EventArgs e)
        {
            //swapDatesLayout aufrufen
            await Navigation.PushModalAsync(new swapDates());
        }
    }

}


