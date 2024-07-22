using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Pegel_Wetter_DFFUDC.RainfallOpenDataViewModel;
using System.Globalization;

namespace Pegel_Wetter_DFFUDC
{
    public class RainfallOpenDataApi
    {
        public List<RainfallOpenDataViewModel> LoadRainfallData(string zipFilePath, string extractPath)
        {
            ZipFile.ExtractToDirectory(zipFilePath, extractPath);

            var pinDataList = new List<RainfallOpenDataViewModel>();

            foreach (var file in Directory.GetFiles(extractPath, "*.txt"))
            {
                var lines = File.ReadAllLines(file);
                foreach (var line in lines)
                {
                    var parts = line.Split(';');
                    if (parts.Length >= 7)
                    {
                        var pinData = new RainfallOpenDataViewModel
                        {
                            StationID = parts[0],
                            StationName = parts[1],
                            Longitude = double.Parse(parts[2], CultureInfo.InvariantCulture),
                            Latitude = double.Parse(parts[3], CultureInfo.InvariantCulture),
                            StationHeight = double.Parse(parts[4], CultureInfo.InvariantCulture),
                            
                        };
                        pinDataList.Add(pinData);
                    }
                }
            }
            return pinDataList;
        }
    }
}

