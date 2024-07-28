
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pegel_Wetter_DFFUDC.Model
{
    public class ModelInputintoHistory : IMeasurementData
    {
        public ObservableCollection<ModelInputintoHistory> ListHistory { get; set; }
        public ModelInputintoHistory() { }

        private static readonly Lazy<ModelInputintoHistory> lazy = new Lazy<ModelInputintoHistory>(() => new ModelInputintoHistory());

        public static ModelInputintoHistory Instance { get { return lazy.Value; } }
        public string edittype { get; set; }
        public string datatype { get; set; }
        public string measurementStationName { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
        public DateTime date { get; set; }
        public string information { get; set; }
        public double measurementData { get; set; }
        public string newedittype { get; set; }
        public string newdatatype { get; set; }
        public string newmeasurementStationName { get; set; }
        public double newlon { get; set; }
        public double newlat { get; set; }
        public DateTime newdate { get; set; }
        public string newinformation { get; set; }
        public double newmeasurementData { get; set; }
        public string StationDetail => $"{edittype}";


        //Added funktioner für feature 2.1
        public void AddWaterLevelData(WaterLevelModel.Root waterLevelData)
        {
            datatype = waterLevelData.datatype;
            measurementStationName = waterLevelData.shortname;
            lon = waterLevelData.longitude;
            lat = waterLevelData.latitude;
            date = waterLevelData.Timestamp;
           // measurementData = waterLevelData.currentMeasurement;
            information = $"{waterLevelData.longname}, {waterLevelData.agency}";


            ListHistory.Add(new ModelInputintoHistory
            {
                edittype = "added",
                datatype = "waterfall",
                measurementStationName = waterLevelData.shortname,
                lon = waterLevelData.longitude,
                lat = waterLevelData.latitude,
                date = waterLevelData.Timestamp,
            //    measurementData =waterLevelData.currentMeasurement,
                information = $"{waterLevelData.longname}, {waterLevelData.agency}"
            });

        }

        public void AddRainfallData(RainfallModel rainfallData)
        {
            datatype = rainfallData.datatype;
            measurementStationName = rainfallData.StationName;
            lon = rainfallData.Longitude;
            lat = rainfallData.Latitude;
            date = rainfallData.FromDate;
            measurementData = rainfallData.StationHight;
            information = $"{rainfallData.edittype}";

            ListHistory.Add(new ModelInputintoHistory
            {
                edittype = "added",
                datatype = "rainfall",
                measurementStationName = rainfallData.StationName,
                lon = rainfallData.Longitude,
                lat = rainfallData.Latitude,
                date = rainfallData.FromDate,
                measurementData = rainfallData.StationHight,
                information = $"{rainfallData.edittype}"
            });

        }
    }
}
