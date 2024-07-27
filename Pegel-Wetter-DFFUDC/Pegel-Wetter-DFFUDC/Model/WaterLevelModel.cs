using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Pegel_Wetter_DFFUDC.Model
{
    public class WaterLevelModel
    {

        // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
        //public class Comment
        //{
        //    public string shortDescription { get; set; }
        //    public string longDescription { get; set; }
        //}
        public class CurrentMeasurement
        {
            public DateTime Timestamp { get; set; }
            public double Value { get; set; }
            //public string stateMnwMhw { get; set; }
            //public string stateNswHsw { get; set; }
        }
        //public class GaugeZero
        //{
        //    public string unit { get; set; }
        //    public double value { get; set; }
        //    public string validFrom { get; set; }
        //}
        public class Root
        {
            //public string uuid { get; set; }
            //public string number { get; set; }
            public string shortname { get; set; }
            public string longname { get; set; }
            //public double km { get; set; }
            public string agency { get; set; }
            public double longitude { get; set; }
            public double latitude { get; set; }
            public Water water { get; set; }
            public List<Timeseries> Timeseries { get; set; }
            //public List<CurrentMeasurement> timeseries { get; set; }
            public CurrentMeasurement currentMeasurement { get; set; }
        }
        public class Water
        {
            public string shortname { get; set; }
            public string longname { get; set; }
        }
        public class Timeseries
        {
            public string shortname { get; set; }
            public string longname { get; set; }
            public string unit { get; set; }
            public int equidistance { get; set; }
            public CurrentMeasurement currentMeasurement { get; set; }
            //public GaugeZero gaugeZero { get; set; }
            //public Comment comment { get; set; }
        }





    }
}
