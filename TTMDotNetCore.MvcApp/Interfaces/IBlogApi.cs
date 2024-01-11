using TTMDotNetCore.MvcApp.Models;
using Refit;
using System.Threading.Tasks;


namespace TTMDotNetCore.MvcApp.Interfaces
{
    public interface IBlogApi
    {
        [Get("/api/blog")]
        Task<BlogResponseModels> GetBlogs();

        [Get("/api/blog/{pageNo}/{pageSize}")]
        Task<BlogResponseModels> GetBlogs(int pageNo, int pageSize = 10);

        [Get("/api/blog/{id}")]
        Task<BlogResponseModel> GetBlog(int id);

        [Post("/api/blog")]
        Task<BlogResponseModel> CreateBlog(BlogDataModel blog);

        [Put("/api/blog/{id}")]
        Task<BlogResponseModel> UpdateBlog(int id, BlogDataModel blog);

        [Patch("/api/blog/{id}")]
        Task<BlogResponseModel> PatchBlog(int id, BlogDataModel blog);

        [Delete("/api/blog/{id}")]
        Task<BlogResponseModel> DeleteBlog(int id);

        [Get("/api/blog/{id}")]
        Task<BlogResponseModel> EditBlog(int id);
	}
}
