namespace Pegel_Wetter_DFFUDC;

  public partial class TestList : ContentPage
  {
    public TestList()
    {
      InitializeComponent();

      List<string> measurementData = new List<string>
      {
        "Pegelstand",
        "Niederschlag",
      };

      testList.ItemsSource = measurementData;
    }
  }