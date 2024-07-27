using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pegel_Wetter_DFFUDC.Model
{
    public class DataStore
    {
        private static readonly Lazy<DataStore> lazy = new Lazy<DataStore>(() => new DataStore());

        public static DataStore Instance => lazy.Value;

        public ObservableCollection<RainfallModel> ListRainfallStation { get; set; }
        public ObservableCollection<WaterLevelModel.Root> ListWaterlevelStation { get; set; }
        public ObservableCollection<ModelInputintoHistory> ListHistory { get; set; }

        private DataStore()
        {
            ListRainfallStation = new ObservableCollection<RainfallModel>();

            ListWaterlevelStation = new ObservableCollection<WaterLevelModel.Root>();

            ListHistory = new ObservableCollection<ModelInputintoHistory>();


            ListRainfallStation = new ObservableCollection<RainfallModel>
            {
              new RainfallModel {datatype="rainfall", StationName = "Alice", Longitude =4, Latitude=256, },
              new RainfallModel {datatype = "rainfall", StationName = "Alice", Longitude = 4, Latitude = 256},
              new RainfallModel {datatype = "rainfall", StationName = "Alice", Longitude = 4, Latitude = 256},
              new RainfallModel {datatype = "rainfall", StationName = "hehe2", Longitude = 3, Latitude = 123},
        };

            ListWaterlevelStation = new ObservableCollection<WaterLevelModel.Root>
        {
              new WaterLevelModel.Root{datatype="waterlevel", longname = "hehe", longitude = 3, latitude = 123},
              new WaterLevelModel.Root{datatype="waterlevel", longname = "hehe", longitude = 3, latitude = 123},
              new WaterLevelModel.Root{datatype="waterlevel", longname = "hehe", longitude = 3, latitude = 123},


        };

            ListHistory = new ObservableCollection<ModelInputintoHistory>()
            {
                new ModelInputintoHistory{edittype = "added",datatype = "waterfall", measurementStationName = "Alice", lon = 22, lat = 123, measurementData =123, information = "angaben"},
                new ModelInputintoHistory{edittype = "edited",datatype = "waterfall", measurementStationName = "Alice", lon = 22, lat = 123, measurementData =123, information = "angaben"},
                new ModelInputintoHistory{edittype = "added",datatype = "rainfall", measurementStationName = "Alice", lon = 22, lat = 123, measurementData =123, information = "angaben"},
                new ModelInputintoHistory{edittype = "deleted",datatype = "rainfall", measurementStationName = "Alice", lon = 22, lat = 123, measurementData =123, information = "angaben"},
                new ModelInputintoHistory{edittype = "added",datatype = "waterfall", measurementStationName = "Alice", lon = 22, lat = 123, measurementData =123, information = "angaben"},
                new ModelInputintoHistory{edittype = "edited",datatype = "rainfall", measurementStationName = "Alice", lon = 22, lat = 123, measurementData =123, information = "angaben"},
                new ModelInputintoHistory{edittype = "added",datatype = "waterfall", measurementStationName = "Alice", lon = 22, lat = 123, measurementData =123, information = "angaben"},
                new ModelInputintoHistory{edittype = "deleted",datatype = "rainfall", measurementStationName = "Alice", lon = 22, lat = 123, measurementData =123, information = "angaben"},

            };


        }


    }

}
