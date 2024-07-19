using Pegel_Wetter_DFFUDC.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pegel_Wetter_DFFUDC.ViewModel
{
    interface InterfaceforHistoryViewModel
    {
        public void ListEdit(ModelInputintoHistory item);

        public void HistoryReturnElement(string edittype);

        public void ListItemShow(ModelInputintoHistory item);
    }

    public class HistoryMethodClass : InterfaceforHistoryViewModel
    {
        public ObservableCollection<ModelInputintoHistory>ListHistory { get; set; }

        public string MeasurementStationName { get; set; }
        public string StationDetail { get; set; }

        public void ListEdit(ModelInputintoHistory item)
        {

        }
        async public void HistoryReturnElement(string edittype)
        {
            switch (edittype)
            {
                case "edited":
                    // man macht die speicherung rückängig
                    break;
                case "added":
                //    ListHistory.RemoveAt(); //nicht LIst History
                    //Liste NOamel Delete
                    break;
                case "deleted":
                  //  ListHistory.RemoveAt(); //nicht LIst History
                   // ListHistory.Add()

                    break;
                default:
                    Console.WriteLine("Unknown edit type");
                    break;
            }
        }
        public void ListItemShow(ModelInputintoHistory item)
        {

        }
    }


}
