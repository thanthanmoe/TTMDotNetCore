using Refit;
using System.Threading.Tasks;
using TTMDotNetCore.MvcApp.Models;

namespace TTMDotNetCore.MvcApp.AppDB
{
	public interface BlogAPI
	{
		[Get("/api/Blog")]
		Task<BlogListResponseModel> GetBlogs();

		[Get("/api/Blog/{pageNo}/{pageSize}")]
		Task<BlogListResponseModel> GetBlog(int pageNo, int pageSize = 10);

		[Get("/api/Blog/{id}")]
		Task<BlogResponseModel> EditBlog(int id);

		[Post("/api/Blog")]
		Task<BlogResponseModel> CreateBlog(BlogDataModel blog);

		[Put("/api/Blog/{id}")]
		Task<BlogResponseModel> UpdateBlog(int id, BlogDataModel blog);

		[Patch("/api/Blog/{id}")]
		Task<BlogResponseModel> PathBlog(int id, BlogDataModel blog);

		[Delete("/api/Blog/{id}")]
		Task<BlogResponseModel> DeleteBlog(int id);
	}
}
