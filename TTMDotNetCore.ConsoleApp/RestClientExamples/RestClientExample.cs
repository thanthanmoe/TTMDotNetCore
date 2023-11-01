using TTMDotNetCore.ConsoleApp.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

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

        private void Update(int id, string title, string author, string content)
        {

        }

        private void Delete(int id)
        {

        }
    }
}
