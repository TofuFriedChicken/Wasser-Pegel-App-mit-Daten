using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Pegel_Wetter_DFFUDC.RainfallStation;
using System.Globalization;
using Microsoft.Maui.Controls.Maps;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Pegel_Wetter_DFFUDC
{
    public class RainfallApi
    {
        private readonly HttpClient _client;
        public RainfallApi()
        {
            _client = new HttpClient();
        }
        public async Task<List<RainfallStations>> GetRainStationsAsync(string url)
        {
            var response = await _client.GetStringAsync(url);
            return ParseStations(response);
        }

        public List<RainfallStations> ParseStations(string data)
        {
            var lines = data.Split('\n');
            var stations = new List<RainfallStations>();

            foreach (var line in lines.Skip(1)) // Skip header line + read lines
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    Debug.WriteLine("Skipping empty line");
                    continue;
                }

                
                var normalizedLine = Regex.Replace(line, @"\s+", " ");      // replace more space
                var columns = normalizedLine.Split(' ');

                if (columns.Length < 9)
                {
                    Debug.WriteLine("Skipping line with insufficient columns: " + line);
                    continue;
                }

                try
                {
                    int stationId = int.Parse(columns[0]);
                    if (stationId < 300 || stationId > 600) continue;

                    var station = new RainfallStations
                    {
                        StationID = stationId,
                        FromDate = DateTime.Parse(columns[1]) , //columns[1],
                        ToDate = DateTime.Parse(columns[2]), //columns[2],
                        StationHight = int.Parse(columns[3]),
                        Latitude = double.Parse(columns[4]),
                        Longitude = double.Parse(columns[5]),
                        StationName = string.Join(" ", columns.Skip(6).Take(columns.Length - 8)),
                        //State = columns[columns.Length - 2],
                        //Abgabe = columns[columns.Length - 1]
                    };

                    stations.Add(station);
                    Debug.WriteLine($"Parsed station: {station.StationName}");      //kontrolle
                }
                catch (FormatException ex)
                {
                    Debug.WriteLine($"Error line: {line} - {ex.Message}");
                    continue;
                }
            }

            return stations;
        }

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

