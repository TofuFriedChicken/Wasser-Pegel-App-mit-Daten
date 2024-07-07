interface IMeasurementData
{
  //information about measurement station
  public string measurementStationName {get; set;}
  public int lon {get; set;}
  public int lat {get; set;}
    
  //further information (date, location or water)
  public int date {get; set;}
  public string information {get; set;} 
    
    //measurement data
  public int measurementData {get; set;}
}