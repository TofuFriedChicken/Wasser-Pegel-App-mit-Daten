using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Microsoft.Maui.Controls.Maps;

namespace Pegel_Wetter_DFFUDC
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();


            var location = new Location(47.6205, -122.3493);

            MyMap_Test.MoveToRegion(MapSpan.FromCenterAndRadius(location, Distance.FromMeters(1)));

            var circle = new Circle
            {
                Center = location,
                Radius = Distance.FromMeters(1000),
                StrokeColor = Color.FromArgb("#88FF0000"),
                StrokeWidth = 8,
                FillColor = Color.FromArgb("#880000FF")

            };

            MyMap_Test.MapElements.Add(circle);

        }

    }

}
