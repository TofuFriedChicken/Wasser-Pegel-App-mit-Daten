using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pegel_Wetter_DFFUDC
{
    public class WaterLevelViewModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
        public class Root
        {
            public string uuid { get; set; }
            public string number { get; set; }
            public string shortname { get; set; }
            public string longname { get; set; }
            public double km { get; set; }
            public string agency { get; set; }
            public double longitude { get; set; }
            public double latitude { get; set; }
            public Water water { get; set; }
        }

        public class Water
        {
            public string shortname { get; set; }
            public string longname { get; set; }
        }

        public ObservableCollection<Root> Pins { get; set; }
        public WaterLevelViewModel()
        {
            Pins = new ObservableCollection<Root>
            {
                new Root { latitude = 0, longitude = 0, shortname= " "},
            };
        }
    }
}
