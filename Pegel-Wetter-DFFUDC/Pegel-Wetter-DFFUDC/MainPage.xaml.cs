using Microsoft.Maui.Maps;
using Microsoft.Maui.Controls.Maps;

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
            //germanMap.Pins.Add(new Pin();
            //{
            //    Location = new({ Binding latitude }, { Binding longitude}),
            //    Label = "Location: " { Binding shortname},
            //})
        }

    }

}
