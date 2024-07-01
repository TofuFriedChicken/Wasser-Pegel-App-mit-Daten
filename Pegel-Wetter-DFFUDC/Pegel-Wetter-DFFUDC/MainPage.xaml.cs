using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Microsoft.Maui.Controls.Maps;

namespace Pegel_Wetter_DFFUDC
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();

            var location = new Location(52.5162, 13.3777);

            MyMap_Test.MoveToRegion(MapSpan.FromCenterAndRadius(location, Distance.FromMeters(1000)));

            var circle = new Circle()
            {
                Center = location,
                Radius = Distance.FromMeters(100000),
                StrokeColor = Color.FromArgb("0000FF"),
                StrokeWidth = 8,
                FillColor = Color.FromArgb("ADD8E6")
            };



            MyMap_Test.MapElements.Add(circle);

        }

    }

}
