namespace Pegel_Wetter_DFFUDC.Model;

public class InputRainfallData : IMeasurementData
{
    public bool Equals(InputRainfallData other)
    {
        if (other == null) return false;
        return (this.measurementStationName.Equals(other.measurementStationName));
    }
    public InputRainfallData() { }

    public string datatype { get; set; }

    public string measurementStationName { get; set; }

    public double lon { get; set; }
    public double lat { get; set; }

    public DateTime date { get; set; }
    public string information { get; set; }

    public double measurementData { get; set; }

    public string StationDetail => $"Messart: {datatype} Stationsname: {measurementStationName} Longitude: {lon} Latitude: {lat} Datum: {date} Information: {information} Messdaten: {measurementData}";
}