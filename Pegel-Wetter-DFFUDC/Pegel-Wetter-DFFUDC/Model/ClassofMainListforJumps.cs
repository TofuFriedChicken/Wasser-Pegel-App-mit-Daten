using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pegel_Wetter_DFFUDC.Model
{
    public class ClassofMainListforJumps
    {
        public ObservableCollection<RainfallViewModeldummy> MainlistScreenshot { get; set; }

        public ClassofMainListforJumps(ObservableCollection<RainfallViewModeldummy> MainlistHistory)
        {
            MainlistScreenshot = new ObservableCollection<RainfallViewModeldummy>(MainlistHistory);
        }
    }
}
