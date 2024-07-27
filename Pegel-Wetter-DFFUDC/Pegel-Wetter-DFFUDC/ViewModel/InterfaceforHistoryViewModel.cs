using Microsoft.Maui.Controls;
using Microsoft.VisualBasic;
using Pegel_Wetter_DFFUDC.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Pegel_Wetter_DFFUDC.ViewModel
{
    interface InterfaceforHistoryViewModel
    {
        public void HistoryReturnElement(ObservableCollection<ModelInputintoHistory> listHistoryparameter, ObservableCollection<Model.RainfallViewModeldummy> inputrainfalldataparameter, ModelInputintoHistory selectedItem);

        public void ListItemShow(ModelInputintoHistory item);
    }

    public class HistoryMethodClass : InterfaceforHistoryViewModel
    {
        public ObservableCollection<ModelInputintoHistory>ListHistory { get; set; }

        public ObservableCollection<Model.RainfallViewModeldummy> ListRainfallStation { get; set; }

        public string MeasurementStationName { get; set; }
        public string StationDetail { get; set; }


        async public void HistoryReturnElement(ObservableCollection<ModelInputintoHistory> listHistoryparameter, ObservableCollection<Model.RainfallViewModeldummy> inputrainfalldataparameter, ModelInputintoHistory selectedItemparameter)
        {
            switch (selectedItemparameter.edittype)
            {
                case "edited":
                    listHistoryparameter.Remove(selectedItemparameter);
                    var itemedited = inputrainfalldataparameter.FirstOrDefault(item => item.measurementStationName == selectedItemparameter.newmeasurementStationName);
                    if (itemedited != null)
                    {
                        inputrainfalldataparameter.Remove(itemedited);
                    }

                    inputrainfalldataparameter.Add(new Model.RainfallViewModeldummy 
                    { 
                        measurementStationName = selectedItemparameter.measurementStationName,
                        lon = selectedItemparameter.lon,
                        lat = selectedItemparameter.lat,
                        date = selectedItemparameter.date,
                        information = selectedItemparameter.information,
                        measurementData = selectedItemparameter.measurementData
                    });
                    break;
                case "added": // in der normalen Liste sollte ein Item erscheinen welches von InputAdd geadded wird. "ListAdd() objects werden dann übertragen in List und ListHistory, wobei in ListHistory das object noch datatype hinzubekommt"
                    var itemToRemove = inputrainfalldataparameter.FirstOrDefault(item => item.measurementStationName == selectedItemparameter.measurementStationName);
                    if (itemToRemove != null)
                    {
                        inputrainfalldataparameter.Remove(itemToRemove);
                    }
                    /*
                    listHistoryparameter.Remove(selectedItemparameter);
                    inputrainfalldataparameter.Remove(new InputRainfallData
                    {
                        measurementStationName = selectedItemparameter.measurementStationName,
                        lon = selectedItemparameter.lon,
                        lat = selectedItemparameter.lat,
                        date = selectedItemparameter.date,
                        information = selectedItemparameter.information,
                        measurementData = selectedItemparameter.measurementData
                    });
                    */
                    break;
                case "deleted":
                    listHistoryparameter.Remove(selectedItemparameter);
                    inputrainfalldataparameter.Add(new Model.RainfallViewModeldummy
                    {
                        datatype = selectedItemparameter.datatype,
                        measurementStationName = "delete return",
                        lon = selectedItemparameter.lon,
                        lat = selectedItemparameter.lat,
                        date = selectedItemparameter.date,
                        information = selectedItemparameter.information,
                        measurementData = selectedItemparameter.measurementData

                    });
                    break;
                default:
                    break;
            }
        }                    
      
        public void ListItemShow(ModelInputintoHistory item)
        {

        }
    }


}
