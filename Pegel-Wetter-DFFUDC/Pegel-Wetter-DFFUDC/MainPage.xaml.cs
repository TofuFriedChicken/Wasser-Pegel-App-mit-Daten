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

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            MyMap_Test.MapElements.Add(circle);

        }

    }

}
