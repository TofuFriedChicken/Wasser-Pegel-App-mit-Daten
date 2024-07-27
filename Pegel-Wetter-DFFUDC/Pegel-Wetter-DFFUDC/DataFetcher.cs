using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pegel_Wetter_DFFUDC
{
    public class DataFetcher
    {
        //private readonly HttpClient _httpClient = new HttpClient();
        //private readonly string _baseUrl = "https://opendata.dwd.de/climate_environment/CDC/observations_germany/climate/daily/more_precip/recent/";

        //public async Task<string> GetRsValueAsync(string stationId)
        //{
        //    var zipFileUrl = $"{_baseUrl}/tageswerte_RR_{stationId}_akt.zip";
        //    var zipFilePath = Path.Combine(FileSystem.CacheDirectory, $"tageswerte_RR_{stationId}_akt.zip");

        //    // Download Zip File
        //    var response = await _httpClient.GetAsync(zipFileUrl);
        //    if (!response.IsSuccessStatusCode) return null;

        //    await using (var fileStream = new FileStream(zipFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
        //    {
        //        await response.Content.CopyToAsync(fileStream);
        //    }
        //    var extractPath = Path.Combine(FileSystem.CacheDirectory, "extracted_files");
        //    ZipFile.ExtractToDirectory(zipFilePath, extractPath, true);

        //    // Find the relevant file
        //    var relevantFile = Directory.GetFiles(extractPath).FirstOrDefault(f => Path.GetFileName(f).StartsWith("produkt_nieder_tag"));
        //    if (relevantFile == null) return null;

        //    // Read and parse the file
        //    var rsValue = await ParseFileForRsValueAsync(relevantFile);

        //    // Clean up
        //    File.Delete(zipFilePath);
        //    Directory.Delete(extractPath, true);
        //    return rsValue;
        //}

        //private async Task<string> ParseFileForRsValueAsync(string filePath)
        //{
        //    var todayDate = DateTime.Now.ToString("yyyyMMdd");
        //    var lines = await File.ReadAllLinesAsync(filePath);
        //    foreach (var line in lines.Skip(1))
        //    {
        //        var columns = line.Split(';');
        //        if (columns.Length > 1 && columns[1] == todayDate)
        //        {
        //            var rsValue = columns[3]; 
        //            return rsValue;
        //        }
        //    }

        //    return null;
        //}

    }
}
