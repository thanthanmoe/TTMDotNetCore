using TTMDotNetCore.ConsoleApp.Models;
using Newtonsoft.Json;
using Refit;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Xml.Linq;


namespace TTMDotNetCore.ConsoleApp.RefitExamples
{
    public class RefitExample
    {
        private readonly IBlogApi blogApi;

        public RefitExample()
        {
            blogApi = RestService.For<IBlogApi>("https://localhost:7253");
        }

        public async Task Run()
        {
            await Read();
            //await Edit(16);
            //await Updte(16,"title", "author", "content");
            //await Create("title", "author", "content");
            //await Delete(16);
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
			BlogResponseModel model = await blogApi.EditBlog(id);

			Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
		}

        private async Task Update(int id, string title, string author, string content)
        {
			BlogResponseModel model = await blogApi.UpdateBlog(id, new BlogDataModel()
			{
				Blog_Title = title,
				Blog_Author = author,
				Blog_Content = content,
			});

			Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
		}

        private async Task Delete(int id)
        {
			BlogResponseModel model = await blogApi.DeleteBlog(id);

			Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
		}

		private async Task Patch(int id, string title, string author, string content)
		{
			BlogResponseModel model = await blogApi.PatchBlog(id, new BlogDataModel()
			{
				Blog_Title = title,
				Blog_Author = author,
				Blog_Content = content,
			});

			Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
		}
	}
}
