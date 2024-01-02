using Microsoft.VisualBasic;

namespace TTMDotNetCore.WebMVCApp.Models
{
    public class ChartJsTimeScaleModel
    {
        public List<string> labels { get; set; }
        public double[][] number { get; set; }
    }
}
