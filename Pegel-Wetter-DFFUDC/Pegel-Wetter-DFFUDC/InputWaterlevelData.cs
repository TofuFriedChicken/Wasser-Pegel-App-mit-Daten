namespace Pegel_Wetter_DFFUDC;

public class InputWaterlevelData : IMeasurementData
{
    public string measurementStationName { get; set; }

    public double lon { get; set; }
    public double lat { get; set; }

    public int date { get; set; }
    public string information { get; set; }

    public double measurementData { get; set; }

    public string StationDetail => $"measurementStationName:{measurementStationName} lon: {lon} lat: {lat} date: {date} inforamtion: {information} measurementData: {measurementData}";

}