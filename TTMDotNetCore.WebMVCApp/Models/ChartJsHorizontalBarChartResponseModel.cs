namespace TTMDotNetCore.WebMVCApp.Models
{
    public class ChartJsHorizontalBarChartResponseModel
    {
        public int DataCount { get; set; }
        public List<string> Labels { get; set; }
        public List<ChartJsHorizontalBarChartModel> DataSets { get; set; }

    }
}
