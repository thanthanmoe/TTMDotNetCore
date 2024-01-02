namespace TTMDotNetCore.WebMVCApp.Models
{
    public class CanvasScatterAndBubbleResponseModel
    {
        public string Title { get; set; }
        public string XAxisTitle { get; set; }
        public string YAxisTitle { get; set; }
        public List<CanvasScatterAndBubbleModel> Data { get; set; }
    }
}
