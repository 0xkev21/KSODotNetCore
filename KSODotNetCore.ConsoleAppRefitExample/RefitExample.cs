using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSODotNetCore.ConsoleAppRefitExample
{
    public class RefitExample
    {
        private readonly IBlogApi _service = RestService.For<IBlogApi>("https://localhost:7107");
        public async Task RunAsync()
        {
            //await ReadAsync();
            //await EditAsync(1);
            //await EditAsync(1000);
            //await CreateAsync("title", "author", "content");
            //await ReadAsync();
            //await UpdateAsync(28, "title", "author", "content");
            //await UpdateAsync(25, "title 3", "author 3", "content 3");
            //await DeleteAsync(28);
            //await DeleteAsync(25);
        }

        private async Task ReadAsync()
        {
            var list = await _service.GetBlogs();
            foreach (var item in list)
            {
                Console.WriteLine($"Id => {item.BlogId}");
                Console.WriteLine($"Title => {item.BlogTitle}");
                Console.WriteLine($"Author => {item.BlogAuthor}");
                Console.WriteLine($"Content => {item.BlogContent}");
                Console.WriteLine("---------------------------------");
            }
        }

        private async Task EditAsync(int id)
        {
            // Refit.ApiException: 'Response status code does not indicate success: 404 (Not Found).'

            try
            {
                var item = await _service.GetBlog(id);
                Console.WriteLine($"Id => {item.BlogId}");
                Console.WriteLine($"Title => {item.BlogTitle}");
                Console.WriteLine($"Author => {item.BlogAuthor}");
                Console.WriteLine($"Content => {item.BlogContent}");
                Console.WriteLine("---------------------------------");
            }
            catch(ApiException ex) // More Specific for Api Exception
            {
                Console.WriteLine(ex.StatusCode.ToString());
                Console.WriteLine(ex.Content);
            }
            catch (Exception ex) // Will catch Non Api exceptions
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task CreateAsync(string title, string author, string content)
        {
            BlogModel blog = new BlogModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content,
            };

            var message = await _service.CreateBlog(blog);
            Console.WriteLine(message);
        }

        private async Task UpdateAsync(int id, string title, string author, string content)
        {
            try
            {
                BlogModel blog = new BlogModel
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content,
                };

                var message = await _service.UpdateBlog(id, blog);
                Console.WriteLine(message);
            }
            catch (ApiException ex)
            {
                Console.WriteLine(ex.StatusCode.ToString());
                Console.WriteLine(ex.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task DeleteAsync(int id)
        {
            try
            {
                var message = await _service.DeleteBlog(id);
                Console.WriteLine(message);
            }
            catch (ApiException ex)
            {
                Console.WriteLine(ex.StatusCode.ToString());
                Console.WriteLine(ex.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
