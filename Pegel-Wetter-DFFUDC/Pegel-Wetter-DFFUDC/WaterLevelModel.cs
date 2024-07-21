using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using static Pegel_Wetter_DFFUDC.WaterLevelViewModel;
using System.Text.Json;
using System.Collections;
using System.Diagnostics;


namespace Pegel_Wetter_DFFUDC
{
    public class WaterLevelModel 
    {
        public WaterLevelApi _waterlevelApi;
        public ObservableCollection<Root> _positions;

        public event PropertyChangedEventHandler PropertyChanged;

        public WaterLevelModel()
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

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

 

