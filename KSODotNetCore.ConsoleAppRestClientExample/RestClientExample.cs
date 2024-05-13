using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace KSODotNetCore.ConsoleAppRestClientExample;

internal class RestClientExample
{

    private readonly RestClient _client = new RestClient(new Uri("https://localhost:7230"));
    private readonly string _blogEndpoint = "api/Blog";

    public async Task RunAsync()
    {
        //await ReadAsync();
        //await EditAsync(1);
        //await EditAsync(100);
        //await CreateAsync("title", "author", "content");
        //await UpdateAsync(24, "title 3", "author 3", "content 3");
    }

    private async Task ReadAsync()
    {
        //RestRequest request = new RestRequest(_blogEndpoint);
        //var response = await _client.GetAsync(request);

        RestRequest request = new RestRequest(_blogEndpoint, Method.Get);
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessStatusCode)
        {
            string jsonStr = response.Content!;
            List<BlogModel> list = JsonConvert.DeserializeObject<List<BlogModel>>(jsonStr)!;
            foreach (var item in list)
            {
                Console.WriteLine($"Title => {item.BlogTitle}");
                Console.WriteLine($"Author => {item.BlogAuthor}");
                Console.WriteLine($"Content => {item.BlogContent}");
                Console.WriteLine("---------------------------------");
            }
        }
    }

    private async Task EditAsync(int id)
    {
        RestRequest request = new RestRequest($"{_blogEndpoint}/{id}", Method.Get);
        var response = await _client.ExecuteAsync(request);
        if (response.IsSuccessStatusCode)
        {
            string jsonStr = response.Content!;
            var item = JsonConvert.DeserializeObject<BlogModel>(jsonStr)!;

            Console.WriteLine($"Title => {item.BlogTitle}");
            Console.WriteLine($"Author => {item.BlogAuthor}");
            Console.WriteLine($"Content => {item.BlogContent}");
            Console.WriteLine("---------------------------------");
        }
        else
        {
            string message = response.Content!;
            Console.WriteLine(message);
        }
    }

    private async Task CreateAsync(string title, string author, string content)
    {
        BlogModel blogModel = new BlogModel()
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content,
        };

        RestRequest request = new RestRequest(_blogEndpoint, Method.Post);
        request.AddJsonBody(blogModel);
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessStatusCode)
        {
            string message = response.Content!;
            Console.WriteLine(message);
        }
    }

    private async Task UpdateAsync(int id, string title, string author, string content)
    {
        BlogModel blogModel = new BlogModel()
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content,
        };

        RestRequest request = new RestRequest($"{_blogEndpoint}/{id}", Method.Put);
        request.AddJsonBody(blogModel);
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessStatusCode)
        {
            string message = response.Content!;
            Console.WriteLine(message);
        }
    }

    private async Task DeleteAsync(int id)
    {
        RestRequest request = new RestRequest($"{_blogEndpoint}/{id}", Method.Delete);
        var response = await _client.ExecuteAsync(request);
        if (response.IsSuccessStatusCode)
        {
            string message = response.Content!;
            Console.WriteLine(message);
            // other processes
            // continue
        }
        else
        {
            string message = response.Content!;
            Console.WriteLine(message);
            // error message
            // break
        }
    }
}
