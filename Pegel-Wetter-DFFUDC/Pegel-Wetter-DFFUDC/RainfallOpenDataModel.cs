using Microsoft.Maui.Controls.Maps;
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
using static Pegel_Wetter_DFFUDC.RainfallOpenDataViewModel;

namespace Pegel_Wetter_DFFUDC
{
    public class RainfallOpenDataModel : INotifyPropertyChanged
    {
        private const string CacheFile = "https://opendata.dwd.de/climate_environment/CDC/observations_germany/climate/daily/more_precip/historical/";
        private const string CacheDateFile = "https://opendata.dwd.de/climate_environment/CDC/observations_germany/climate/daily/more_precip/historical/RR_Tageswerte_Beschreibung_Stationen.txt";

        public ObservableCollection<RainfallOpenDataViewModel> RainfallData { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public RainfallOpenDataModel()
        {
            RainfallData = new ObservableCollection<RainfallOpenDataViewModel>();
        }

        public async Task LoadDataAsync()
        {
            if (IsCacheValid())
            {
                LoadFromCache();
            }
            else
            {
                var api = new RainfallOpenDataApi();
                var data = await api.GetRainfallDataAsync();
                RainfallData.Clear();
                foreach (var item in data)
                {
                    RainfallData.Add(item);
                }
                SaveToCache(data);
                UpdateCacheDate();
            }
        }
        private bool IsCacheValid()
        {
            if (File.Exists(CacheDateFile))
            {
                var cachedDate = DateTime.Parse(File.ReadAllText(CacheDateFile));
                return (DateTime.Now - cachedDate).TotalDays < 1;
            }
            return false;
        }

        private void LoadFromCache()
        {
            if (File.Exists(CacheFile))
            {
                var json = File.ReadAllText(CacheFile);
                var data = JsonConvert.DeserializeObject<List<RainfallOpenDataViewModel>>(json);
                RainfallData.Clear();
                foreach (var item in data)
                {
                    RainfallData.Add(item);
                }
            }
        }

        private void SaveToCache(List<RainfallOpenDataViewModel> data)
        {
            var json = JsonConvert.SerializeObject(data);
            File.WriteAllText(CacheFile, json);
        }

        private void UpdateCacheDate()
        {
            File.WriteAllText(CacheDateFile, DateTime.Now.ToString());
        }
    }
}



