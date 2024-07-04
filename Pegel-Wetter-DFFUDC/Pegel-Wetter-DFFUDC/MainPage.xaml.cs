using Microsoft.Maui.Maps;

namespace Pegel_Wetter_DFFUDC
{
    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            InitializeComponent();
            //center germany
            var location = new Location(51,10);
            var mapSpan = new MapSpan(location, 500000000, 500000000);
            germanMap.MoveToRegion(mapSpan);

        }
        private void GoCurrent(object sender, EventArgs e)
        {

        }

    }

}
