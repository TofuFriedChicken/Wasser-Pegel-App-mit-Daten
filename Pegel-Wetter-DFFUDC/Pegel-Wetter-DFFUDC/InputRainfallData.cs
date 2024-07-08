namespace Pegel_Wetter_DFFUDC;

public class InputRainfallData : IMeasurementData
{
  public string measurementStationName { get; set; }

  public double lon { get; set; }
  public double lat { get; set; }

  public int date { get; set; }
  public string information { get; set; }

  public double measurementData { get; set; }
}