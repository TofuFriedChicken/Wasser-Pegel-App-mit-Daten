
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pegel_Wetter_DFFUDC
{
    public class RainfallOpenDataViewModel
    {

        //public DateTime Date { get; set; }

        public string StationID { get; set; }
        public string StationName { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double StationHeight { get; set; }
        public double Value { get; set; }
        //public string StartDate { get; set; }
        //public string EndDate { get; set; }
        //public string DeviceType { get; set; }
        //public string MeasurementMethod { get; set; }

    }
}
