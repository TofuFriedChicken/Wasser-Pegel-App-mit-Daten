using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pegel_Wetter_DFFUDC.Model
{
    public class ClassofHistoryforJumps
    {
        public ObservableCollection<ModelInputintoHistory> ListHistoryScreenshot { get; set; }

        public ClassofHistoryforJumps(ObservableCollection<ModelInputintoHistory> listHistory)
        {
            ListHistoryScreenshot = new ObservableCollection<ModelInputintoHistory>(listHistory);
        }
    }
}
