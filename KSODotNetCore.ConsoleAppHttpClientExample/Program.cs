using KSODotNetCore.ConsoleAppHttpClientExample;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

Console.WriteLine("Hello, World!");

// Console App - Client (Frontend)
// ASP.Net Core Web API - Server (Backend)

//HttpClient client = new HttpClient();

//var task = client.GetAsync("https://localhost:7230/api/Blog"); // Job
//task.RunSynchronously();

//var response = await client.GetAsync("https://localhost:7230/api/Blog");
//if(response.IsSuccessStatusCode)
//{
//    string jsonStr = await response.Content.ReadAsStringAsync();
//    //Console.WriteLine(jsonStr);
//    List<BlogModel> list = JsonConvert.DeserializeObject<List<BlogModel>>(jsonStr)!;
//    foreach(var blog in list)
//    {
//        //Console.WriteLine(JsonConvert.SerializeObject(blog));
//        Console.WriteLine($"Title => {blog.BlogTitle}");
//        Console.WriteLine($"Author => {blog.BlogAuthor}");
//        Console.WriteLine($"Content => {blog.BlogContent}");
//        Console.WriteLine("---------------------------------");
//    }
//}

HttpClientExample httpClientExample = new HttpClientExample();
await httpClientExample.RunAsync();

Console.ReadLine();