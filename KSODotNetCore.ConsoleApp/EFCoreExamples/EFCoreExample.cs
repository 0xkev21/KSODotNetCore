using KSODotNetCore.ConsoleApp.Dtos;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSODotNetCore.ConsoleApp.EFCoreExamples
{
    internal class EFCoreExample
    {

        private readonly AppDbContext db;

        public EFCoreExample()
        {
            db = new AppDbContext();
        }
        public void Run()
        {
            //Read();
            //Edit(1);
            //Edit(11);
            //Update(11, "title 2", "author 2", "content 2");
            Delete(11);
        }

        private void Read()
        {
            var list = db.Blogs.ToList();
            foreach (BlogDto item in list)
            {
                Console.WriteLine($"Blog Id: {item.BlogId}");
                Console.WriteLine($"Blog Title: {item.BlogTitle}");
                Console.WriteLine($"Blog Author: {item.BlogAuthor}");
                Console.WriteLine($"Blog Title: {item.BlogContent}");
                Console.WriteLine("-------------------------------");
            }
        }

        private void Edit(int id)
        {
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                Console.WriteLine("No data found.");
                return;
            }

            Console.WriteLine($"Blog Id: {item.BlogId}");
            Console.WriteLine($"Blog Title: {item.BlogTitle}");
            Console.WriteLine($"Blog Author: {item.BlogAuthor}");
            Console.WriteLine($"Blog Title: {item.BlogContent}");
        }

        private void Create(string title, string author, string content)
        {

            var item = new BlogDto()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            db.Blogs.Add(item);
            int result = db.SaveChanges();
            string message = result > 0 ? "Saving success." : "Saving failed.";
            Console.WriteLine(message);
        }

        private void Update(int id, string title, string author, string content)
        {
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                Console.WriteLine("No data found.");
                return;
            }

            item.BlogTitle = title;
            item.BlogAuthor = author;
            item.BlogContent = content;

            int result = db.SaveChanges();
            string message = result > 0 ? "Updating success." : "Updating failed.";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                Console.WriteLine("No data found.");
                return;
            }

            db.Blogs.Remove(item);
            int result = db.SaveChanges();
            string message = result > 0 ? "Deleting success." : "Deleting failed.";
            Console.WriteLine(message);
        }
    }
}
