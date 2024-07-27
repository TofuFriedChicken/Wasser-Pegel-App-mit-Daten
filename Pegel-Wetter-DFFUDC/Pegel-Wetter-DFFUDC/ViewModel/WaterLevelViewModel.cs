using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using static Pegel_Wetter_DFFUDC.Model.WaterLevelModel;
using System.Text.Json;
using System.Collections;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Pegel_Wetter_DFFUDC
{
    public class WaterLevelViewModel
        
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public WaterLevelViewModel()
        {
            _waterlevelApi = new WaterLevelApi();
        }

        public ObservableCollection<Root> Positions
        { 
            get => _positions;
            set
            {
                _positions = value;
                OnPropertyChanged();
            }
        }
        

        public async Task LoadWaterLevels()
        {
            var waterLevels = await _waterlevelApi.GetWaterLevelsAsync();
            Positions = new ObservableCollection<Root>(waterLevels);
        }





    }
}
