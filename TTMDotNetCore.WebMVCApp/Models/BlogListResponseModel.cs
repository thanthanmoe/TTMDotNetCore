namespace TTMDotNetCore.WebMVCApp.Models
{
    public class BlogListResponseModel
    {
        public List<BlogDataModel> BlogList { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int PageRowCount { get; set; }
    }
}
