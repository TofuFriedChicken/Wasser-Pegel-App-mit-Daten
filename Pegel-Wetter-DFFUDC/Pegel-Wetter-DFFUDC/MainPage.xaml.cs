namespace Pegel_Wetter_DFFUDC
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            try
            {
                var lineSeries = new LineSeries<double>
                {
                    Values = new double[] { 3, 5, 7, 4, 3, 6, 8 }
                };

                cartesianChart.Series = new ISeries[] { lineSeries };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }

}
