using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.Maui.Controls.Maps;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Pegel_Wetter_DFFUDC
{
    public class RainfallApi
    {
        public async Task<string[]> LoadFileFromUrlAsync(string url)
        {
            using HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return content.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}

