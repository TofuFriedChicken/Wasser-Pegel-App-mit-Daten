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
        public void ListEdit(ModelInputintoHistory item);

        public void HistoryReturnElement(ObservableCollection<ModelInputintoHistory> listHistoryparameter, ObservableCollection<InputRainfallData> inputrainfalldataparameter, ModelInputintoHistory selectedItem);

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
        async public void HistoryReturnElement(ObservableCollection<ModelInputintoHistory> listHistoryparameter, ObservableCollection<InputRainfallData> inputrainfalldataparameter, ModelInputintoHistory selectedItemparameter)
        {
            switch (selectedItemparameter.edittype)
            {
                case "edited":
                    // man macht die speicherung rückängig
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
                    break;
                case "added": // in der normalen Liste sollte ein Item erscheinen welches von InputAdd geadded wird. "ListAdd() objects werden dann übertragen in List und ListHistory, wobei in ListHistory das object noch datatype hinzubekommt"
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
                    break;
                case "deleted":
                    listHistoryparameter.Remove(selectedItemparameter);
                    inputrainfalldataparameter.Add(new InputRainfallData
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
        /*
                    listHistoryparameter.Add(new ModelInputintoHistory
                    {
                        edittype = "added", // Setze edittype auf "added" oder was passend ist
                        datatype = "rainfall",
                        measurementStationName = "ich wurde geadded",
                        lon = 3,
                        lat = 123,
                        date = 1123,
                        information = "123",
                        measurementData = 123
                    });
                    */                  
        //    ListHistory.RemoveAt(); //nicht LIst History
                    //Liste NOamel Delete
        public void ListItemShow(ModelInputintoHistory item)
        {

        }
    }


}
