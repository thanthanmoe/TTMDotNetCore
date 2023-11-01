using TTMDotNetCore.ConsoleApp.Models;
using Newtonsoft.Json;
using Refit;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace TTMDotNetCore.ConsoleApp.RefitExamples
{
    public class RefitExample
    {
        private readonly IBlogApi blogApi;

        public RefitExample()
        {
            blogApi = RestService.For<IBlogApi>("http://localhost:7223");
        }

        public async Task Run()
        {
            await Read();
        }

        private async Task Read()
        {
            try
            {
                BlogListResponseModel model = await blogApi.GetBlogs();
                Console.WriteLine(JsonConvert.SerializeObject(model));
                Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
            }
            catch (ApiException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            
        }

        private async Task Create(string title, string author, string content)
        {
            BlogResponseModel model = await blogApi.CreateBlog(new BlogDataModel
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content,
            });
            Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
        }

        private async Task Edit(int id)
        {
           
        }

        private void Update(int id, string title, string author, string content)
        {

        }

        private void Delete(int id)
        {

        }
    }

    public interface IBlogApi
    {
        [Get("/api/blog")]
        Task<BlogListResponseModel> GetBlogs();

        [Get("/api/blog/{pageNo}/{pageSize}")]
        Task<BlogListResponseModel> GetBlogs(int pageNo, int pageSize = 10);

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
    }
}
