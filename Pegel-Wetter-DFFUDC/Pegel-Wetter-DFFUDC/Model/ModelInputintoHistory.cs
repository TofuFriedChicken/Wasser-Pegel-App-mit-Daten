using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pegel_Wetter_DFFUDC.Model
{
    public class ModelInputintoHistory : IMeasurementData
    {
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

        public string StationDetail => $"{edittype}";
    }
}
