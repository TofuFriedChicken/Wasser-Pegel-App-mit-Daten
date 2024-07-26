using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pegel_Wetter_DFFUDC
{
    //public class FileProcessor
    //{
    //    private readonly HttpClient _httpClient;

    //    public FileProcessor()
    //    {
    //        _httpClient = new HttpClient();
    //    }

    //    public async Task ProcessFileAsync(string stationId)
    //    {
    //        string baseUrl = "https://opendata.dwd.de/climate_environment/CDC/observations_germany/climate/daily/more_precip/recent/";
    //        string zipFileName = $"tageswerte_RR_{stationId.PadLeft(5, '0')}_akt.zip";
    //        string zipUrl = $"{baseUrl}{zipFileName}";

    //        try
    //        {
    //            byte[] zipBytes = await _httpClient.GetByteArrayAsync(zipUrl);

    //            using (var zipStream = new MemoryStream(zipBytes))
    //            using (var archive = new ZipArchive(zipStream))
    //            {
    //                string targetFileName = GenerateFileName(stationId);
    //                var entry = archive.GetEntry(targetFileName);

    //                if (entry != null)
    //                {
    //                    using (var reader = new StreamReader(entry.Open()))
    //                    {
    //                        while (!reader.EndOfStream)
    //                        {
    //                            string line = await reader.ReadLineAsync();
    //                            if (line.StartsWith("STATIONS_ID") || string.IsNullOrWhiteSpace(line))
    //                                continue;

    //                            var columns = line.Split(';');
    //                            if (columns.Length >= 4 && columns[1] == DateTime.Now.ToString("yyyyMMdd"))
    //                            {
    //                                string date = columns[1];
    //                                string rsValue = columns[3];

    //                                await DisplayAlert("RS Wert", $"Datum: {date}\nRS Wert: {rsValue}", "OK");
    //                                return;
    //                            }
    //                        }

    //                        await DisplayAlert("RS Wert", "Keine Daten für das aktuelle Datum gefunden.", "OK");
    //                    }
    //                }
    //                else
    //                {
    //                    await DisplayAlert("Fehler", $"Die Datei {targetFileName} wurde im ZIP-Ordner nicht gefunden.", "OK");
    //                }
    //            }
    //        }
    //        catch (HttpRequestException e)
    //        {
    //            await DisplayAlert("Fehler", $"Fehler beim Herunterladen der Datei: {e.Message}", "OK");
    //        }
    //        catch (Exception e)
    //        {
    //            await DisplayAlert("Fehler", $"Allgemeiner Fehler: {e.Message}", "OK");
    //        }
    //    }

    //    private string GenerateFileName(string stationId)
    //    {
    //        string currentDate = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
    //        return $"produkt_nieder_tag_{currentDate}_{stationId.PadLeft(5, '0')}";
    //    }

    //    private Task DisplayAlert(string title, string message, string cancel)
    //    {
    //        // Dies ist ein Platzhalter für eine tatsächliche Implementierung in .NET MAUI,
    //        // die eine Nachricht an den Benutzer anzeigt, z.B. Application.Current.MainPage.DisplayAlert.
    //        Console.WriteLine($"{title}: {message}");
    //        return Task.CompletedTask;
    //    }
    //}
}
