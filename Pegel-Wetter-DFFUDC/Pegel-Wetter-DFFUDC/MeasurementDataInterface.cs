namespace Pegel_Wetter_DFFUDC;

  interface IMeasurementData
  {
  //information about measurement station
  public string measurementStationName { get; set; }
  public double lon { get; set; }
  public double lat { get; set; }

  //further information (date, location or water)
  public DateTime date { get; set; }
  public string information { get; set; }

  //measurement data
  public double measurementData { get; set; }
  }



