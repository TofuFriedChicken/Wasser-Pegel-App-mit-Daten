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
        public class RainfallStation
        {
            //public string Station_id {  get; set; }
            public DateTime von_datum { get; set; }  // maybe string
            public DateTime bis_datum { get; set; }
            //public int Stationshoehe {  get; set; }
            public double geoBreite { get; set; }
            public double geoLaenge { get; set; }
            public string Stationsname { get; set; }  // stadt
            //public string Bundesland {  get; set; }
        }
    }
}
