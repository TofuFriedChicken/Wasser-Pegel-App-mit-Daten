
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pegel_Wetter_DFFUDC
{
    public class RainfallStations
    {

        public int StationID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int StationHight { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string StationName { get; set; }

    }

    
    public class RainfallStation
    {

            public int StationId { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public int StationHeight { get; set; }
            
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public string Stationname { get; set; }
            public string State { get; set; }

            public string Abgabe { get; set; }
       
    }
    

}
