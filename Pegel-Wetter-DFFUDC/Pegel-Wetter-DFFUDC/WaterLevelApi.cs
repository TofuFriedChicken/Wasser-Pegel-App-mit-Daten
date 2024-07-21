using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Pegel_Wetter_DFFUDC.WaterLevelViewModel;

namespace Pegel_Wetter_DFFUDC
{
    public class WaterLevelApi
    {
        private readonly HttpClient _httpClient;
        public WaterLevelApi()
        {
            _httpClient = new HttpClient();
        }
        public async Task<List<Root>> GetWaterLevelsAsync()
        {
            var url = "https://pegelonline.wsv.de/webservices/rest-api/v2/stations.json?includeTimeseries=true&includeCurrentMeasurement=true";
            var response = await _httpClient.GetStringAsync(url);
            //var positions = JsonConvert.DeserializeObject<List<Root>>(response);
            return JsonConvert.DeserializeObject<List<Root>>(response);
        }
    }

}
