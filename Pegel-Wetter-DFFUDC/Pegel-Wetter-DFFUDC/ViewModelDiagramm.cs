using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;



namespace Pegel_Wetter_DFFUDC
{
    public partial class ViewModelDiagramm : ObservableObject
    {
        public ISeries[] Series { get; set; }
            = new ISeries[]
            {
                new LineSeries<double>
                {
                    Values = new double[] { 4, 6, 5, 3, -3, -1, 2 }
                },
                new ColumnSeries<double>
                {
                    Values = new double[] { 2, 5, 4, -2, 4, -3, 5 }
                }
            };
        public Axis[] XAxes { get; set; } =
        {
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
                LabelsPaint = new SolidColorPaint
                {
                    Color = SKColors.Blue,
                    FontFamily = "Times New Roman",
                    SKFontStyle = new SKFontStyle(SKFontStyleWeight.ExtraBold, SKFontStyleWidth.Normal, SKFontStyleSlant.Italic)
                },
            }
        };
    }
}
