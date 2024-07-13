namespace Pegel_Wetter_DFFUDC.Model;

public class InputWaterlevelData : IMeasurementData
{
    public string datatype { get; set; }

    public string measurementStationName { get; set; }

    public double lon { get; set; }
    public double lat { get; set; }

    public int date { get; set; }
    public string information { get; set; }

    public double measurementData { get; set; }

    public string StationDetail => $"Messart: {datatype} Stationsname: {measurementStationName} Longitude: {lon} Latitude: {lat} Datum: {date} Inforamtion: {information} Messdaten: {measurementData}";

}