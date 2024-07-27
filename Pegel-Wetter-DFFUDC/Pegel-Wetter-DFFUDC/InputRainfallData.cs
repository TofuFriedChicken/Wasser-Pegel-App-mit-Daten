namespace Pegel_Wetter_DFFUDC.Model;

public class InputRainfallData : IMeasurementData
{
    public string datatype { get; set; }

    public string measurementStationName { get; set; }

    public double lon { get; set; }
    public double lat { get; set; }

    public DateTime date { get; set; }
    public string information { get; set; }

    public double measurementData { get; set; }

}