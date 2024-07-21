using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;

namespace Pegel_Wetter_DFFUDC
{
    public class ViewModelDiagramm
    {
        public ISeries[] Series { get; set; }
            = new ISeries[]
            {
                new LineSeries<int>
                {
                    Values = new int[] { 4, 6, 5, 3, -3, -1, 2 }
                },
                new ColumnSeries<double>
                {
                    Values = new double[] { 2, 5, 4, -2, 4, -3, 5 }
                }
            };
    }
}
