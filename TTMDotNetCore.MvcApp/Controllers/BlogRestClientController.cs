using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Xml;
using TTMDotNetCore.MvcApp.Models;

namespace TTMDotNetCore.MvcApp.Controllers
{
	public class BlogRestClientController : Controller
	{
		private readonly RestClient _restClient;

		public BlogRestClientController(RestClient restClient)
		{
			_restClient = restClient;
		}

		public async Task<IActionResult> Index()
		{

			BlogListResponseModel model = new BlogListResponseModel();
			RestRequest request = new RestRequest("/api/Blog", Method.Get);
			//RestResponse response = await client.GetAsync(request);
			RestResponse response = await _restClient.ExecuteAsync(request);

			if (response.IsSuccessStatusCode)
			{
				string jsonStr = response.Content!;
				//Json to C# object
				//SearilizeObject => C# to json
				//DeserilizeObject => json to C#
				model = JsonConvert.DeserializeObject<BlogListResponseModel>(jsonStr)!;
			}

			TempData["ControllerName"] = "BlogRestClient";
			return View("~/Views/BlogRefit/Index.cshtml", model);
		}

		public IActionResult CreateForm()
		{
			TempData["ControllerName"] = "BlogRestClient";
			return View("~/Views/BlogRefit/Create.cshtml");
		}

		public async Task<IActionResult> Create(BlogDataModel reqModel)
		{
			RestRequest request = new RestRequest("/api/Blog", Method.Post);
			request.AddBody(reqModel);
			RestResponse response = await _restClient.ExecuteAsync(request);

			return Redirect("/BlogRestClient");
		}

		public async Task<IActionResult> Edit(int id)
		{
			BlogResponseModel model = new BlogResponseModel();
			RestRequest request = new RestRequest($"/api/Blog/{id}", Method.Get);
			//RestResponse response = await client.GetAsync(request);
			RestResponse response = await _restClient.ExecuteAsync(request);

			if (response.IsSuccessStatusCode)
			{
				string jsonStr = response.Content!;
				//Json to C# object
				//SearilizeObject => C# to json
				//DeserilizeObject => json to C#
				model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr)!;
				Console.WriteLine(JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.Indented));
			}

			TempData["ControllerName"] = "BlogRestClient";
			return View("~/Views/BlogRefit/Edit.cshtml", model);
		}

		public async Task<IActionResult> Update(int id, BlogDataModel reqModel)
		{
			RestRequest request = new RestRequest($"/api/Blog/{id}", Method.Put);
			request.AddBody(reqModel);
			RestResponse response = await _restClient.ExecuteAsync(request);

			return Redirect("/BlogRestClient");
		}

		public async Task<IActionResult> Delete(int id)
		{
			RestRequest request = new RestRequest($"/api/Blog/{id}", Method.Delete);
			//RestResponse response = await client.GetAsync(request);
			RestResponse response = await _restClient.ExecuteAsync(request);

			return Redirect("/BlogRestClient");
		}

	}
}