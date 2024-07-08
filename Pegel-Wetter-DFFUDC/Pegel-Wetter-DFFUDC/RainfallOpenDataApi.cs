using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Pegel_Wetter_DFFUDC.RainfallOpenDataViewModel;

namespace Pegel_Wetter_DFFUDC
{
    public class RainfallOpenDataApi
    {
        

        private readonly HttpClient _httpClient;
        public RainfallOpenDataApi()
        {
            _httpClient = new HttpClient();
        }
        //public async Task<List<RainfallStation>> GetRainfallAsync()
        //{
        //    var url = "https://opendata.dwd.de/climate_environment/CDC/observations_germany/climate/monthly/more_precip/historical/";
        //    var response = await _httpClient.GetStringAsync(url);
        //    return JsonConvert.DeserializeObject<List<RainfallStation>>(response);
        //}
        public async Task<List<RainfallStation>> GetRainfallAsync()
        {
            var url = "https://opendata.dwd.de/climate_environment/CDC/observations_germany/climate/monthly/more_precip/historical/";

            try
            {
                var response = await _httpClient.GetStringAsync(url);
                if (string.IsNullOrEmpty(response))
                {
                    throw new Exception("Die API Antwort ist leer.");
                }
                // Um meine Api zu überprüfen mit Fehlermeldung
                Console.WriteLine(response);

                return JsonConvert.DeserializeObject<List<RainfallStation>>(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Es hab ein Fehler beim Abrufen der Daten: {ex.Message}");
                return null;
            }
        }
    }
}
