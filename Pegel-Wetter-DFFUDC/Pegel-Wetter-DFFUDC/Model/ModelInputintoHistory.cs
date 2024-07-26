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
     //   public ObservableCollection<ModelInputintoHistory> ListHistory { get; set; }
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
    }
}
