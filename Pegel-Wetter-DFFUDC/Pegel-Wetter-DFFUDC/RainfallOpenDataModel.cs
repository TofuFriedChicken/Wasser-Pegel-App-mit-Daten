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
    public class RainfallOpenDataModel
    {

        private readonly RainfallOpenDataApi _rainfallApi;

        public ObservableCollection<RainfallOpenDataViewModel> RainfallDataCollection { get; private set; }

        public RainfallOpenDataModel()
        {
            _rainfallApi = new RainfallOpenDataApi();
            RainfallDataCollection = new ObservableCollection<RainfallOpenDataViewModel>();
        }

        public void LoadData(string zipStream, string extractPath)
        {
            var data = _rainfallApi.LoadRainfallData(zipStream, extractPath);
            RainfallDataCollection.Clear();

            foreach (var item in data)
            {
                RainfallDataCollection.Add(item);
            }
        }
    }
}



