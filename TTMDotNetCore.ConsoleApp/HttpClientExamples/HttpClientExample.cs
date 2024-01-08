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
            await Read();
            await Edit(1);
            await Create("title", "author", "content");
        }

        private async Task Read()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("http://localhost:7223/api/blog");
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                // json to C# object
                // encode
                // decode
                // encrypt
                // decrypt
                // SerializeObject => C# to json
                // DeserializeObject => json to C#
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
            var response = await client.PostAsync($"http://localhost:7223/api/blog", httpContent);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                BlogResponseModel model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
                Console.WriteLine(JsonConvert.SerializeObject(model));
                Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
            }
        }

        private async Task Edit(int id)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"http://localhost:7223/api/blog/{id}");
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                BlogResponseModel model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
                Console.WriteLine(JsonConvert.SerializeObject(model));
                Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
            }
        }

        private async Task UpdateAsync(int id, string title, string author, string content)
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
			HttpResponseMessage response = await client.PutAsync($"https://localhost:7244/api/blog/{id}", httpContent);
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

        private async Task DeleteAsync(int id)
        {
			HttpClient client = new HttpClient();
			HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7244/api/blog/{id}");
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
