
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
        public void HistoryReturnElementrainfall(ObservableCollection<ModelInputintoHistory> listHistoryparameter, ObservableCollection<RainfallModel> inputrainfalldataparameter, ModelInputintoHistory selectedItem);
        public void HistoryReturnElementwaterlevel(ObservableCollection<ModelInputintoHistory> listHistoryparameter, ObservableCollection<WaterLevelModel.Root> inputwaterleveldataparameter, ModelInputintoHistory selectedItem);

    }

    public class HistoryMethodClass : InterfaceforHistoryViewModel
    {
        public ObservableCollection<ModelInputintoHistory> ListHistory { get; set; }

        public ObservableCollection<RainfallModel> ListRainfallStation { get; set; }

        public string MeasurementStationName { get; set; }
        public string StationDetail { get; set; }

        public void HistoryReturnElementwaterlevel(ObservableCollection<ModelInputintoHistory> listHistoryparameter, ObservableCollection<WaterLevelModel.Root> inputwaterleveldataparameter, ModelInputintoHistory selectedItem)
        {

            switch (selectedItem.edittype)
            {
                case "edited":
                    listHistoryparameter.Remove(selectedItem);
                    var itemedited = inputwaterleveldataparameter.FirstOrDefault(item => item.longname == selectedItem.measurementStationName);
                    if (itemedited != null)
                    {
                        inputwaterleveldataparameter.Remove(itemedited);
                    }

                    inputwaterleveldataparameter.Add(new WaterLevelModel.Root
                    {
                        longname = selectedItem.measurementStationName,
                        longitude = selectedItem.lon,
                        latitude = selectedItem.lat,
                        Timestamp = selectedItem.date,
                        //     information = selectedItemparameter.information,
                        //     measurementData = selectedItemparameter.measurementData
                    });
                    break;
                case "added": // in der normalen Liste sollte ein Item erscheinen welches von InputAdd geadded wird. "ListAdd() objects werden dann übertragen in List und ListHistory, wobei in ListHistory das object noch datatype hinzubekommt"
                    var itemToRemove = inputwaterleveldataparameter.FirstOrDefault(item => item.longname == selectedItem.measurementStationName);
                    if (itemToRemove != null)
                    {
                        inputwaterleveldataparameter.Remove(itemToRemove);
                    }
                    break;
                case "deleted":
                    listHistoryparameter.Remove(selectedItem);
                    inputwaterleveldataparameter.Add(new WaterLevelModel.Root
                    {
                        datatype = selectedItem.datatype,
                        longname = selectedItem.measurementStationName,
                        longitude = selectedItem.lon,
                        latitude = selectedItem.lat,
                        Timestamp = selectedItem.date,
                        //       information = selectedItemparameter.information,
                        //       measurementData = selectedItemparameter.measurementData

                    });
                    break;
                default:
                    break;
            }
        }
        async public void HistoryReturnElementrainfall(ObservableCollection<ModelInputintoHistory> listHistoryparameter, ObservableCollection<RainfallModel> inputrainfalldataparameter, ModelInputintoHistory selectedItemparameter)
        {
            if (selectedItemparameter.datatype == "rainfall")
            {
                switch (selectedItemparameter.edittype)
                {
                    case "edited":
                        listHistoryparameter.Remove(selectedItemparameter);
                        var itemedited = inputrainfalldataparameter.FirstOrDefault(item => item.StationName == selectedItemparameter.measurementStationName);


                        if (itemedited != null)
                        {
                            inputrainfalldataparameter.Remove(itemedited);
                        }

                        inputrainfalldataparameter.Add(new RainfallModel
                        {
                            StationName = selectedItemparameter.measurementStationName,
                            Longitude = selectedItemparameter.lon,
                            Latitude = selectedItemparameter.lat,
                            FromDate = selectedItemparameter.date,
                            //     information = selectedItemparameter.information,
                            //     measurementData = selectedItemparameter.measurementData
                        });
                        break;
                    case "added": // in der normalen Liste sollte ein Item erscheinen welches von InputAdd geadded wird. "ListAdd() objects werden dann übertragen in List und ListHistory, wobei in ListHistory das object noch datatype hinzubekommt"
                        var itemToRemove = inputrainfalldataparameter.FirstOrDefault(item => item.StationName == selectedItemparameter.measurementStationName);
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
                        inputrainfalldataparameter.Add(new RainfallModel
                        {
                            datatype = selectedItemparameter.datatype,
                            StationName = selectedItemparameter.measurementStationName,
                            Longitude = selectedItemparameter.lon,
                            Latitude = selectedItemparameter.lat,
                            FromDate = selectedItemparameter.date,
                            //       information = selectedItemparameter.information,
                            //       measurementData = selectedItemparameter.measurementData

                        });
                        break;
                    default:
                        break;


                }

            }

        }


    }


}


