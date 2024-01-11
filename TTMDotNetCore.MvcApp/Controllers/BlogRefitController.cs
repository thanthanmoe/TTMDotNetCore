using Microsoft.AspNetCore.Mvc;
using TTMDotNetCore.MvcApp.Models;
using TTMDotNetCore.MvcApp.Interfaces;

namespace TTMDotNetCore.MvcApp.Controllers
{
	public class BlogRefitController : Controller
	{
		private readonly IBlogApi _blogAPI;

		public BlogRefitController(IBlogApi blogApi)
		{
			_blogAPI = blogApi;
		}

		public async Task<IActionResult> Index()
		{
			BlogResponseModels model= await _blogAPI.GetBlogs();
			TempData["ControllerName"] = "BlogRefit";
			return View(model);
		}

		public IActionResult Create()
		{
			TempData["ControllerName"] = "BlogRefit";
			return View("Create");
		}

		public async Task<IActionResult> Save(BlogDataModel reqModel)
		{
			BlogResponseModel model = await _blogAPI.CreateBlog(reqModel);
			return Redirect("/BlogRefit/Index");
		}

		public async Task<IActionResult> Edit(int id)
		{
			BlogResponseModel model = await _blogAPI.EditBlog(id);
			TempData["ControllerName"] = "BlogRefit";
			return View(model);
		}

		public async Task<IActionResult> Update(int id, BlogDataModel reqModel)
		{
			BlogResponseModel model = await _blogAPI.UpdateBlog(id, reqModel);

			return Redirect("/BlogRefit");
		}

		public async Task<IActionResult> Delete(int id)
		{
			BlogResponseModel model = await _blogAPI.DeleteBlog(id);

			return Redirect("/BlogRefit/Index");
		}

	}
}
