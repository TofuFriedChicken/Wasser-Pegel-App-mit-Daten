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

        public ObservableCollection<RainfallViewModeldummy> ListRainfallStation { get; set; }
        public ObservableCollection<WaterlevelViewModeldummy> ListWaterlevelStation { get; set; }
        public ObservableCollection<ModelInputintoHistory> ListHistory { get; set; }

        private DataStore()
        {
            ListRainfallStation = new ObservableCollection<RainfallViewModeldummy>();

            ListWaterlevelStation = new ObservableCollection<WaterlevelViewModeldummy>();

            ListHistory = new ObservableCollection<ModelInputintoHistory>();


            ListRainfallStation = new ObservableCollection<RainfallViewModeldummy>
            {
              new RainfallViewModeldummy { datatype="rainfall", measurementStationName = "Alice", lon=4, lat=256, information="6", measurementData=2},
              new RainfallViewModeldummy { datatype="rainfall", measurementStationName = "Alice", lon=4, lat=256, information="6", measurementData=2},
              new RainfallViewModeldummy { datatype="rainfall", measurementStationName = "Alice", lon=4, lat=256, information="6", measurementData=2},
              new RainfallViewModeldummy { datatype="waterlevel", measurementStationName = "hehe2", lon = 3, lat = 123, information = "123", measurementData = 123 },
        };

            ListWaterlevelStation = new ObservableCollection<WaterlevelViewModeldummy>()
        {
              new WaterlevelViewModeldummy {datatype="waterlevel", measurementStationName = "hehe", lon = 3, lat = 123, information = "123", measurementData = 123 },
        };

            ListHistory = new ObservableCollection<ModelInputintoHistory>()
        {
                          new ModelInputintoHistory {edittype="deleted", datatype="waterlevel", measurementStationName = "hehe3", lon = 3, lat = 123, information = "123", measurementData = 123 },

              new ModelInputintoHistory {edittype="edited", datatype="rainfall", measurementStationName = "hehe1", lon = 3, lat = 123, information = "123", measurementData = 123 },
              new ModelInputintoHistory {edittype="added", datatype="waterlevel", measurementStationName = "hehe2", lon = 3, lat = 123, information = "123", measurementData = 123 },
              new ModelInputintoHistory {edittype="deleted", datatype="waterlevel", measurementStationName = "hehe3", lon = 3, lat = 123, information = "123", measurementData = 123 },
              new ModelInputintoHistory {edittype="edited", datatype="rainfall", measurementStationName = "hehe1", lon = 3, lat = 123, information = "123", measurementData = 123 },
              new ModelInputintoHistory {edittype="added", datatype="waterlevel", measurementStationName = "hehe2", lon = 3, lat = 123, information = "123", measurementData = 123 },

        };


        }


    }

}
