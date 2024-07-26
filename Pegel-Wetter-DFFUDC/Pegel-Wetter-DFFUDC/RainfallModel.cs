using Microsoft.Maui.Controls.Maps;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Formats.Asn1;
using System.Globalization;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Pegel_Wetter_DFFUDC.RainfallStation;

namespace Pegel_Wetter_DFFUDC
{
    public class RainfallModel
    {
        private readonly RainfallApi _rainfallApi;
        public RainfallModel(RainfallApi rainfallApi)
        {
            _rainfallApi = rainfallApi;
        }
        public async Task<List<RainfallStations>> GetRainStationsAsync(string url)
        {
            return await _rainfallApi.GetRainStationsAsync(url);
        }

       
        public RainfallStations[] ProcessLines(string[] lines)
        {
            var processedLines = lines
                .Skip(380)
                .Take(580)
                .Select(line =>
                {
                    var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    return new RainfallStations
                    {
                        StationID = int.Parse(parts[0]),
                        FromDate = DateTime.ParseExact(parts[1], "yyyyMMdd", CultureInfo.InvariantCulture),
                        ToDate = DateTime.ParseExact(parts[2], "yyyyMMdd", CultureInfo.InvariantCulture),
                        StationHight = int.Parse(parts[3]),
                        Latitude = double.Parse(parts[4], CultureInfo.InvariantCulture),
                        Longitude = double.Parse(parts[5], CultureInfo.InvariantCulture),
                        StationName = parts[6]
                    };
                })
                .ToArray();
            return processedLines;
        }
    }
}



