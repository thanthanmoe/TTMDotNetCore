using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Text;
using TTMDotNetCore.MvcApp.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TTMDotNetCore.MvcApp.Controllers
{
    public class BlogHttpClientController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public BlogHttpClientController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            //_httpClient.BaseAddress = new Uri(_configuration.GetSection("RestApiUrl").Value!);
        }

        public async Task<IActionResult> Index()
        {
            BlogResponseModels model = new BlogResponseModels();
            HttpResponseMessage response = await _httpClient.GetAsync("api/blog");
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<BlogResponseModels>(jsonStr)!;
            }
            TempData["ControllerName"] = "BlogHttpClient";
            return View("~/Views/BlogRefit/Index.cshtml", model);
        }

        public IActionResult Create()
        {
            TempData["ControllerName"] = "BlogHttpClient";
            return View("~/Views/BlogRefit/Create.cshtml");
        }

        public async Task<IActionResult> Save(BlogDataModel reqModel)
        {
          
            string blog = JsonConvert.SerializeObject(reqModel);
            HttpContent content = new StringContent(blog, Encoding.UTF8, Application.Json);
            HttpResponseMessage response = await _httpClient.PostAsync($"/api/blog", content);
            TempData["ControllerName"] = "BlogHttpClient";
            return Redirect("/BlogHttpClient");
        }

        public async Task<IActionResult> Edit(int id)
        {
          
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/blog/{id}");
            BlogResponseModel model = new BlogResponseModel();
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr)!;
            }

            TempData["ControllerName"] = "BlogHttpClient";
            return View("~/Views/BlogRefit/Edit.cshtml", model);
        }

        public async Task<IActionResult> Update(int id, BlogDataModel reqModel)
        {
            string blog = JsonConvert.SerializeObject(reqModel);
            HttpContent content = new StringContent(blog, Encoding.UTF8, Application.Json);
            HttpResponseMessage response = await _httpClient.PutAsync($"/api/blog/{id}", content);

            return Redirect("/BlogHttpClient");
        }

        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/Student/{id}");

            return Redirect("/BlogHttpClient");
        }

    }
}