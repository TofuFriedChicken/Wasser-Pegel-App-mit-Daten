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
        public ObservableCollection<InputRainfallData> MainlistScreenshot { get; set; }

        public ClassofMainListforJumps(ObservableCollection<InputRainfallData> MainlistHistory)
        {
            MainlistScreenshot = new ObservableCollection<InputRainfallData>(MainlistHistory);
        }
    }
}
