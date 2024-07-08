using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Pegel_Wetter_DFFUDC.RainfallOpenDataViewModel;

namespace Pegel_Wetter_DFFUDC
{
    public class RainfallOpenDataModel
    {
        public RainfallOpenDataApi _rainfallApi;
        public ObservableCollection<RainfallStation> _positions;

        public event PropertyChangedEventHandler PropertyChanged;

        public RainfallOpenDataModel()
        {
            _rainfallApi = new RainfallApi();
        }

        public ObservableCollection<RainfallStation> Positions
        {
            get => _positions;

            set
            {
                _positions = value;
                OnPropertyChanged();
            }
        }


        public async Task LoadRainfall()
        {
            var rainfalls = await _rainfallApi.GetRainfallAsync();
            Positions = new ObservableCollection<RainfallStation>(rainfalls);
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    internal class RainfallApi : RainfallOpenDataApi
    {
    }
}
