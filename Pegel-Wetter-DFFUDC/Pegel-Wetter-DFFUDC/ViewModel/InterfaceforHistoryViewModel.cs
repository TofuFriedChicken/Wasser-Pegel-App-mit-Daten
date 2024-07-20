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
                    inputrainfalldataparameter.Add(new InputRainfallData 
                    { 
                        measurementStationName = "ich wurde geadded",
                        lon = selectedItemparameter.lon,
                        lat = selectedItemparameter.lat,
                        date = selectedItemparameter.date,
                        information = selectedItemparameter.information,
                        measurementData = selectedItemparameter.measurementData
                    });

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
                    break;
                case "added":
                    //    ListHistory.RemoveAt(); //nicht LIst History
                    //Liste NOamel Delete
                    listHistoryparameter.Remove(selectedItemparameter);
                    listHistoryparameter.Add(selectedItemparameter);

                    break;
                case "deleted":
                    listHistoryparameter.Remove(selectedItemparameter);
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



                    //  ListHistory.RemoveAt(); //nicht LIst History
                    // ListHistory.Add()

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
