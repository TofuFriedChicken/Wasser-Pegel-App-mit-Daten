using System.Collections.ObjectModel;

namespace Pegel_Wetter_DFFUDC.Model;

public class RainfallViewModeldummy : IMeasurementData
{
    //  public ObservableCollection<InputRainfallData> ListRainfallStation { get; set; }

    public bool Equals(RainfallViewModeldummy other)
    {
        if (other == null) return false;
        return (this.measurementStationName.Equals(other.measurementStationName));
    }

    private static readonly Lazy<RainfallViewModeldummy> lazy = new Lazy<RainfallViewModeldummy>(() => new RainfallViewModeldummy());

    public static RainfallViewModeldummy Instance { get { return lazy.Value; } }

    public string datatype { get; set; }

    public string measurementStationName { get; set; }

    public double lon { get; set; }
    public double lat { get; set; }

    public DateTime date { get; set; }
    public string information { get; set; }

    public double measurementData { get; set; }

    public string StationDetail => $"Messart: {datatype} Stationsname: {measurementStationName} Longitude: {lon} Latitude: {lat} Datum: {date} Information: {information} Messdaten: {measurementData}";
}