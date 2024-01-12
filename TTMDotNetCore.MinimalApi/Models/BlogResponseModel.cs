namespace TTMDotNetCore.MinimalApi.Models
{
    public class BlogResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public BlogDataModel Data { get; set; }
    }
    public class BlogResponseModels
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<BlogDataModel> Data { get; set; }
    }
    public class BlogListResponseModel
    {
        public List<BlogDataModel> BlogList { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int PageRowCount { get; set; }
    }
}
