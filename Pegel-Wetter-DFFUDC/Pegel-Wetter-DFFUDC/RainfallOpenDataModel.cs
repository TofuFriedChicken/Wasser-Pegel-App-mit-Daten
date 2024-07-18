using Microsoft.Maui.Controls.Maps;
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
        public ObservableCollection<RainfallOpenDataModel> RainfallData { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public RainfallOpenDataModel()
        {
            RainfallData = new ObservableCollection<RainfallOpenDataModel>();
        }

        //public async Task LoadDataAsync(RainfallOpenDataViewModel item)
        //{
        //    var api = new RainfallOpenDataApi();
        //    var data = await api.GetRainfallDataAsync();
        //    RainfallData.Clear();
        //    foreach (var item in data)
        //    {
        //        RainfallData.Add(item);
        //    }
        //}
    }


}
