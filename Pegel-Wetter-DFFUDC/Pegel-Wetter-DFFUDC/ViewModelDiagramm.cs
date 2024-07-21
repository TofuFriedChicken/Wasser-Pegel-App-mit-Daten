using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using LiveChartsCore.Defaults;

namespace Pegel_Wetter_DFFUDC
{
    public partial class ViewModelDiagramm : ObservableObject
    {
        public ISeries[] Series { get; set; }
            = new ISeries[]
            {
                /*new ColumnSeries<DateTimePoint>
                {
                    Values = new ObservableCollection<DateTimePoint>
                    {
                        new DateTimePoint(new DateTime(2021, 1, 1), 3),
                        new DateTimePoint(new DateTime(2021, 1, 2), 6),
                        new DateTimePoint(new DateTime(2021, 1, 3), 5),
                        new DateTimePoint(new DateTime(2021, 1, 4), 3),
                        new DateTimePoint(new DateTime(2021, 1, 5), 5),
                        new DateTimePoint(new DateTime(2021, 1, 6), 8),
                        new DateTimePoint(new DateTime(2021, 1, 7), 6)
                    }
                },*/ //example date scale from: https://livecharts.dev/docs/Maui/2.0.0-rc2/samples.axes.dateTimeScaled
                new LineSeries<double>
                {
                    Values = new double[] { 4, 6, 5, 3, -3, -1, 2 }
                },
                new ColumnSeries<double>
                {
                    Values = new double[] { 2, 5, 4, -2, 4, -3, 5 }
                } //example from: https://livecharts.dev/docs/Maui/2.0.0-rc2/Overview.Installation
            };
        public Axis[] XAxes { get; set; } =
        {
            /*new DateTimeAxis(TimeSpan.FromDays(1), date => date.ToString("MMMM dd")),*/
            new Axis
            {
                Name = "Datum",
                Labels = new string[] {"heute", "morgen"}, //include dateTime
                LabelsRotation =15,
            }
        };
        public Axis[] YAxes { get; set; } =
        {
            new Axis
            {
                Name = "Niederschlag in mm / Pegelstand in m",
                /*LabelsPaint = new SolidColorPaint
                {
                    Color = SKColors.Blue,
                    FontFamily = "Times New Roman",
                    SKFontStyle = new SKFontStyle(SKFontStyleWeight.ExtraBold, SKFontStyleWidth.Normal, SKFontStyleSlant.Italic)
                },*/
            }
        };
    }
}
