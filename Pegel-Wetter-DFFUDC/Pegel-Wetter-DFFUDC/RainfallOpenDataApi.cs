using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Pegel_Wetter_DFFUDC.RainfallOpenDataViewModel;

namespace Pegel_Wetter_DFFUDC
{
    public class RainfallOpenDataApi
    {
        public async Task<List<RainfallOpenDataViewModel>> GetRainfallDataAsync()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Pegel_Wetter_DFFUDC.Resources.Zips.Rainfall.zip"; 

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null) //test exe
                    throw new FileNotFoundException("Data not found");

                return await ExtractRainfallDataFromZip(stream);
            }
        }

        private async Task<List<RainfallOpenDataViewModel>> ExtractRainfallDataFromZip(Stream zipStream)
        {
            var rainfallData = new List<RainfallOpenDataViewModel>();

            using (var archive = new ZipArchive(zipStream))
            {
                foreach (var entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith(".csv"))  // überprüfung necesary!
                    {
                        using (var reader = new StreamReader(entry.Open()))
                        {
                            var header = await reader.ReadLineAsync(); 
                            while (!reader.EndOfStream)
                            {
                                var line = await reader.ReadLineAsync();
                                var values = line.Split(',');

                                rainfallData.Add(new RainfallOpenDataViewModel
                                {
                                    Label = values[0],
                                    Latitude = double.Parse(values[1]),
                                    Longitude = double.Parse(values[2]),
                                    Value = double.Parse(values[3])
                                });
                            }
                        }
                    }
                }
            }

            return rainfallData;
        }
    }

}

