using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using LiveChartsCore.Defaults;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System;
using System.ComponentModel;
using System.Dynamic;

namespace Pegel_Wetter_DFFUDC
{
    public partial class ViewModelDiagramm : ObservableObject
    {
        private readonly DataProviderLevel _dataProviderLevel;
        private readonly ObservableCollection<ObservableValue> _observableValues;
        //private readonly Date _date;
        //private readonly RSValue _rsValue;

        private readonly ObservableCollection<string> _dates;
        private readonly ObservableCollection<string> _rsValues;

        public ISeries[] Rainfall { get; private set; }
        public ISeries[] Level { get; private set; }

        private int _startIndex;
        public int StartIndex           //sets the Startpoint of the diagram Points
        {
            get => _startIndex;
            set
            {
                SetProperty(ref _startIndex, value);
                UpdateSeries();
            }
        }

        private int _count;
        public int Count            //sets how many diagram points the user would like to see
        {
            get => _count;
            set
            {
                SetProperty(ref _count, value);
                UpdateSeries();
            }
        }

        public ViewModelDiagramm()
        {
            _dataProviderLevel = new DataProviderLevel();
            _observableValues = new ObservableCollection<ObservableValue>();
            _dates = new ObservableCollection<string>(GetInitialDates());
            _rsValues = new ObservableCollection<string>(GetInitialRSValues());


            _startIndex = 0;
            _count = 20;                                                                                    //Inital amount of Values of the Diagram


            Level = new ISeries[]                                                                           //creates the diagram for the Waterlevel which
            {
                new LineSeries<DateTimePoint>
                {
                    Values = _dataProviderLevel.GetLevelData()
                }
            };
        }

        /*public string GetSummary()
        {
            string datesSummary = string.Join(", ", Dates);
            string rsValuesSummary = string.Join(", ", RSValues);
            return $"Dates: {datesSummary}\nRS Values: {rsValuesSummary}";
        }*/                                                                                                 //used for mainPage Display Alert

        private List<string> GetInitialRSValues()                                                           //sets Inital Values for _rsValues
        {
            return new List<string>
            {
                "0.0",
                "0.0",
                "0.0",
                "9.0",
                "0.4",
                "32.3",
                "0.1",
                "0.9",
                "0.0",
                "3.5",
                "0.0",
                "0.0",
                "0.0",
                "0.0",
                "0.2",
                "0.0",
                "5.1",
                "0.0",
                "0.0",
                "2.0"
            };
        }

        private List<string> GetInitialDates()                                                              //sets Inital Values for _dates
        {
            return new List<string>
            {
                "20240707", "20240708", "20240709", "20240710", "20240711", "20240712", "20240713", "20240714", "20240715", "20240716", "20240717", "20240718", "20240719", "20240720", "20240721", "20240722", "20240723", "20240724", "20240725", "20240726"
            };
        }

        public void UpdateRSValues(List<string> rsValues)                                                   //updates Values for _rsValues
        {

            foreach (var value in rsValues)
            {
                _rsValues.Add(value);
            }

        }

        public void UpdateDates(List<string> dates)                                                         //updates Values for _dates
        {
            foreach (var value in dates)
            {
                _dates.Add(value);
            }
        }

        private void UpdateSeries()                                                                         //updates the diagram
        {
            if (_startIndex < 0) _startIndex = 0;                                                           //sets startIndex to 0 if smaller then 0
            if (_startIndex >= _rsValues.Count) _startIndex = _rsValues.Count - 1;
            if (_count < 0) _count = 1;                                                                     //sets the count to 1 as the smallest possible count is 0 and it seems that the user Input a wrong value for _count
            if (_startIndex + _count > _rsValues.Count) _count = _rsValues.Count - _startIndex;             //sets the count to the maximum amount of Values left in _rsValues from start Index

            _observableValues.Clear();                                                                      //clears all ObservableValues in _observableValues

            for (int i = StartIndex; i < StartIndex + Count && i < _rsValues.Count; i++)                    //in new ObservableValues from _rsValues into _observableValues
            {
                if (double.TryParse(_rsValues[i], out double result))
                {
                    _observableValues.Add(new ObservableValue(result / 10));
                }
            }

            Rainfall = new ISeries[]                                                                        //creates the diagram for rainfall
            {
                new LineSeries<ObservableValue>
                {
                Values = _observableValues,
                Fill = null
                }
            };

            XAxesRainfall = new Axis[]                                                                      //sets the dates on the Xaxis
            {
                new Axis
                {
                    Name = "Datum",
                    Labels = _dates.Skip(StartIndex).Take(Math.Min(Count, _dates.Count - StartIndex)).ToArray(),
                    LabelsRotation = 15,
                }
            };                                                                                              //line 131-170 based off https://livecharts.dev/docs/Maui/2.0.0-rc2/samples.lines.autoupdate

            OnPropertyChanged(nameof(Rainfall));
            OnPropertyChanged(nameof(XAxesRainfall));

        }



        public Axis[] XAxesRainfall { get; private set; }

        public Axis[] XAxesLevel { get; set; } =                                                           //sets the DateTime on the XAxis
        {
           new Axis
           {
               Name = "Datum",
               Labeler = value =>
               {
                   try
                   {
                       var dateTime = new DateTime((long)value);
                       return dateTime.ToString("dd.MM.yyyy");
                   }
                   catch (ArgumentOutOfRangeException)
                   {
                       return string.Empty;                                                                 //line 178-196 based off https://github.com/beto-rodriguez/LiveCharts2/discussions/281
                   }
               },
               LabelsRotation = 15,
               UnitWidth = TimeSpan.FromDays(1).Ticks,
               MinStep = TimeSpan.FromDays(1).Ticks
           }
       };

        public Axis[] YAxesRainfall { get; set; } =                                                         //Adds the Text on YAxes
        {
           new Axis
           {
               Name = "Niederschlag in mm",
           }
       };

        public Axis[] YAxesLevel { get; set; } =                                                            //Adds the Text on YAxes
        {
           new Axis
           {
               Name = "Pegelstand in cm",
           }
       };
    }

    public class DatensatzRegen
    {
        List<string> lastLines = new List<string>();
        Queue<string> lineQueue = new Queue<string>();
    }

    public class DataProviderLevel                                                                          //Provides the values for the Waterlevel diagram
    {
        public List<DateTimePoint> GetLevelData()
        {
            return new List<DateTimePoint>
            {
                new DateTimePoint(new DateTime(2023, 5, 18), 2),
                new DateTimePoint(new DateTime(2023, 5, 19), 5),
                new DateTimePoint(new DateTime(2023, 5, 20), 4),
                new DateTimePoint(new DateTime(2023, 5, 21), 0),
                new DateTimePoint(new DateTime(2023, 5, 22), 4),
                new DateTimePoint(new DateTime(2023, 5, 23), 0),
                new DateTimePoint(new DateTime(2023, 5, 24), 5)
            };
        }                                                                                                   //line 224-235 besed off https://livecharts.dev/docs/Maui/2.0.0-rc2/samples.axes.dateTimeScaled
    }

    /*public class RSValue
    {
        public List<string> rSValues()
        {
            return new List<string>
            {
                "0.0",
                "0.0",
                "0.0",
                "9.0",
                "0.4",
                "32.3",
                "0.1",
                "0.9",
                "0.0",
                "3.5",
                "0.0",
                "0.0",
                "0.0",
                "0.0",
                "0.2",
                "0.0",
                "5.1",
                "0.0",
                "0.0",
                "2.0"
            };
        }
    }

    public class Date
    {
        public List<string> Dates()
        {
            return new List<string>
            {
                "20240707",
                "20240708",
                "20240709",
                "20240710",
                "20240711",
                "20240712",
                "20240713",
                "20240714",
                "20240715",
                "20240716",
                "20240717",
                "20240718",
                "20240719",
                "20240720",
                "20240721",
                "20240722",
                "20240723",
                "20240724",
                "20240725",
                "20240726"
            };
        }
    }*/
}

