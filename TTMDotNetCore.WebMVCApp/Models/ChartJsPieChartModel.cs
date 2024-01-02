namespace TTMDotNetCore.WebMVCApp.Models
{
    public class ChartJsPieChartModel
    {
        public List<string> Labels { get; set; }
        public string Label { get; set; }
        public List<int> Data { get; set; }
        public List<string> BackgroundColor { get; set; }
    }
}
