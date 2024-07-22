using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pegel_Wetter_DFFUDC
{
    public class WaterLevelCashing
    {
        private Dictionary<string, WaterLevelViewModel> _cache = new Dictionary<string, WaterLevelViewModel>();
        public WaterLevelViewModel Get(string key)
        {
            _cache.TryGetValue(key, out var pinData);
            return pinData;
        }
        public void Add(string key, WaterLevelViewModel pinData)
        {
            if (!_cache.ContainsKey(key))
            {
                _cache[key] = pinData;
            }
        }
        public bool Contains(string key)
        {
            return _cache.ContainsKey(key);
        }
    }
}
