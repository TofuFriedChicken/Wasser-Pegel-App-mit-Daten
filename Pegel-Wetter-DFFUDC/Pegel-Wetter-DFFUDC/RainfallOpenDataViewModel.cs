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

            public string StationId { get; set; }
            public string Label { get; set; }
            //public string Address { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }

            public DateTime Date { get; set; }
            public double Value { get; set; }
        
    }
}
