using System.Collections.ObjectModel;

namespace Pegel_Wetter_DFFUDC.Model;

public class WaterlevelModeldummy : IMeasurementData
{
  //  public ObservableCollection<InputWaterlevelData> ListWaterlevelStation { get; set; }

    public WaterlevelModeldummy() { }

    private static readonly Lazy<WaterlevelModeldummy> lazy = new Lazy<WaterlevelModeldummy>(() => new WaterlevelModeldummy());

    public static WaterlevelModeldummy Instance { get { return lazy.Value; } }


    public string datatype { get; set; }

    public string measurementStationName { get; set; }

    public double lon { get; set; }
    public double lat { get; set; }

    public DateTime date { get; set; }
    public string information { get; set; }

    public double measurementData { get; set; }

    public string StationDetail => $"Messart: {datatype} Stationsname: {measurementStationName} Longitude: {lon} Latitude: {lat} Datum: {date} Information: {information} Messdaten: {measurementData}";

}