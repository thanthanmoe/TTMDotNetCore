using TTMDotNetCore.ConsoleApp.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Http;

namespace TTMDotNetCore.ConsoleApp.RestClientExamples
{
    public class RestClientExample
    {
        public async Task Run()
        {
            await Read();
            await Edit(1);
            await Create("title", "author", "content");
        }

        private async Task Read()
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest("http://localhost:7223/api/blog", Method.Get);
            //await client.GetAsync(request);
            RestResponse response = await client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr =  response.Content;
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
            RestClient client = new RestClient();
            RestRequest request = new RestRequest("http://localhost:7223/api/blog", Method.Post);
            request.AddBody(blog);
            RestResponse response = await client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content;
                BlogResponseModel model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
                Console.WriteLine(JsonConvert.SerializeObject(model));
                Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
            }
        }

        private async Task Edit(int id)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"https://localhost:7098/api/blog/{id}", Method.Get);
            RestResponse response = await client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content;
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
