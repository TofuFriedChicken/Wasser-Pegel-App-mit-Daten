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

    async void OnDeleteClicked (object sender, EventArgs e)
    {
      bool deleteData = await DisplayAlert("Datensatz löschen", "Willst du diese Daten wirklich löschen? Sie können danach nicht wieder hergestellt werden.", "Löschen", "Abbrechen");     //source display alert: https://learn.microsoft.com/de-de/dotnet/maui/user-interface/pop-ups?view=net-maui-8.0#display-an-alert (last visist: 14.07.24)
      
      if (deleteData)
      {
        test.Text=" ";
        //use .Remove source: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.remove?view=net-8.0 (last visit: 14.07.24)
      }
      

    }

    
  }
  
public class MeasurementData
{
  public string measurementData { get; set; }
 
}