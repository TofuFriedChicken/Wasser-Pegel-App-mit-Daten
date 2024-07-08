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
        public async Task<List<RainfallStation>> GetRainfallAsync()
        {
            var url = "https://opendata.dwd.de/climate_environment/CDC/observations_germany/climate/monthly/more_precip/historical/";
            var response = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<RainfallStation>>(response);
        }
    }
}
