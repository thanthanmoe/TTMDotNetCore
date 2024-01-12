using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TTMDotNetCore.ConsoleApp.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TTMDotNetCore.ConsoleApp.HttpClientExamples
{
    public class HttpClientExample
    {
        public async Task Run()
        {
            //await Read();
            await Create("title", "author", "content");
            //await Edit(16);
            //await Updte(16,"title", "author", "content");
            //await Delete(16);

        }

        private async Task Read()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://localhost:7253/api/blog");
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
               
                BlogListResponseModel model = JsonConvert.DeserializeObject<BlogListResponseModel>(jsonStr);
                Console.WriteLine(JsonConvert.SerializeObject(model));
                Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
            }
        }

        private async Task Create(string title, string author, string content)
        {
            BlogDataModel blog = new BlogDataModel
            {
                Blog_Author = author,
                Blog_Content = content,
                Blog_Title = title
            };
            string blogJson = JsonConvert.SerializeObject(blog);
            HttpContent httpContent = new StringContent(blogJson, Encoding.UTF8, Application.Json);

            HttpClient client = new HttpClient();
            var response = await client.PostAsync($"https://localhost:7253/api/blog", httpContent);
            if (response.IsSuccessStatusCode)
            {
              
                string jsonStr = await response.Content.ReadAsStringAsync();
                BlogResponseModel model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
                await Console.Out.WriteLineAsync(model.Message);
                Console.WriteLine(JsonConvert.SerializeObject(model));
                Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
            }
        }

        private async Task Edit(int id)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"https://localhost:7253/api/blog/{id}");
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                BlogResponseModel model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
                Console.WriteLine(JsonConvert.SerializeObject(model));
                Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
            }
        }

        private async Task Update(int id, string title, string author, string content)
        {
			BlogDataModel blog = new BlogDataModel
			{
				Blog_Title = title,
				Blog_Author = author,
				Blog_Content = content
			};
			string jsonBlog = JsonConvert.SerializeObject(blog);
			HttpContent httpContent = new StringContent(jsonBlog, Encoding.UTF8, Application.Json);

			HttpClient client = new HttpClient();
			HttpResponseMessage response = await client.PutAsync($"https://localhost:7253/api/blog/{id}", httpContent);
			if (response.IsSuccessStatusCode)
			{
				string jsonStr = await response.Content.ReadAsStringAsync();
				var model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
				await Console.Out.WriteLineAsync(model.Message);
			}
			else
			{
				string jsonStr = await response.Content.ReadAsStringAsync();
				var model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
				Console.WriteLine(model.Message);
			}
		}

        private async Task Delete(int id)
        {
			HttpClient client = new HttpClient();
			HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7253/api/blog/{id}");
			if (response.IsSuccessStatusCode)
			{
				string jsonStr = await response.Content.ReadAsStringAsync();
				var model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
				Console.WriteLine(model.Message);
			}
			else
			{
				string jsonStr = await response.Content.ReadAsStringAsync();
				var model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
				Console.WriteLine(model.Message);
			}
		}
    }
}
