//using Microsoft.Maui.Controls.Maps;
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
        public async Task<List<RainfallStation>> GetRainStationsAsync(string url)
        {
            return await _rainfallApi.GetRainStationsAsync(url);
        }
    }
}



