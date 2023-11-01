using System.Collections.Generic;

namespace TTMDotNetCore.ConsoleApp.Models
{
    public class BlogResponseModel
    {
    public bool IsSuccess { get; set; } 
    public string Message { get; set; }
    public BlogDataModel Data{ get; set; }
    }
    public class BlogListResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<BlogDataModel> Data { get; set; }
    }
}
