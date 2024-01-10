using Microsoft.AspNetCore.Mvc;
using TTMDotNetCore.MvcApp.AppDB;
using TTMDotNetCore.MvcApp.Models;
namespace TTMDotNetCore.MvcApp.Controllers
{
	public class BlogRefitController : Controller
	{
		private readonly BlogAPI _blogApi;

		public BlogRefitController(BlogAPI blogApi)
		{
			_blogApi = blogApi;
		}

		public async Task<IActionResult> Index()
		{
			var model = await _blogApi.GetBlogs();
			TempData["ControllerName"] = "Blogrefit";
			return View(model);
		}

		public IActionResult CreateForm()
		{
			TempData["ControllerName"] = "Blogrefit";
			return View("Create");
		}

		public async Task<IActionResult> Create(BlogDataModel reqModel)
		{
			BlogResponseModel model = await _blogApi.CreateBlog(reqModel);
			return Redirect("/Blogrefit");
		}

		public async Task<IActionResult> Edit(int id)
		{
			BlogResponseModel model = await _blogApi.EditBlog(id);
			TempData["ControllerName"] = "Blogrefit";
			return View(model);
		}

		public async Task<IActionResult> Update(int id, BlogDataModel reqModel)
		{
			BlogResponseModel model = await _blogApi.UpdateBlog(id, reqModel);

			return Redirect("/Blogrefit");
		}

		public async Task<IActionResult> Delete(int id)
		{
			BlogResponseModel model = await _blogApi.DeleteBlog(id);

			return Redirect("/Blogrefit");
		}

	}
}
