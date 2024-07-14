namespace Pegel_Wetter_DFFUDC;

  public partial class TestList : ContentPage
  {
    public TestList()
    {
      InitializeComponent();


      List<MeasurementData> measurementDatas = new List<MeasurementData>  //source fill List: https://learn.microsoft.com/de-de/dotnet/maui/user-interface/controls/listview?view=net-maui-8.0 (last visit: 14.07.24)
      {
        new MeasurementData {measurementData = "10mm"},
        new MeasurementData {measurementData = "5cm üNN"}
      };

      testList.ItemsSource = measurementDatas;
    }

    private void OnDeleteClicked (object sender, EventArgs e)
    {
      test.Text=" ";
    }

    
  }
  
public class MeasurementData
{
  public string measurementData { get; set; }
 
}