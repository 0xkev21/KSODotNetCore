﻿using KSODotNetCore.RestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using KSODotNetCore.Shared;
using static KSODotNetCore.Shared.AdoDotNetService;

namespace KSODotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNet2Controller : ControllerBase
    {
        private readonly AdoDotNetService _adoDotNetService = new AdoDotNetService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from tbl_blog";
            var list = _adoDotNetService.Query<BlogModel>(query);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {

            {
                var item = FindById(id);
                if(item is null)
                {
                    return NotFound("No data found.");
                }
                return Ok(item);
            }
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

            int result = _adoDotNetService.Execute(query,
                new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameter("@BlogContent", blog.BlogContent)
            );

            string message = result > 0 ? "Creating Success." : "Creating Failed.";
            return Ok(message);

            //return StatusCode(200, message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel blog)
        {
            var item = FindById(id);
            if(item is null)
            {
                return NotFound("No data found.");
            }

            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";
            SqlConnection connection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            blog.BlogId = id;
            int result = _adoDotNetService.Execute(query,
                new AdoDotNetParameter("@BlogId", blog.BlogId),
                new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameter("@BlogContent", blog.BlogContent)
            );
            string message = result > 0 ? "Updating Successful" : "Updating Failed";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blog)
        {
            var item = FindById(id);
            if(item is null)
            {
                return NotFound("No data found.");
            }

            int parametersCount = 1;
            string conditions = string.Empty;
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                conditions += " [BlogTitle] = @BlogTitle, ";
                parametersCount++;
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                conditions += " [BlogAuthor] = @BlogAuthor, ";
                parametersCount++;
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                conditions += " [BlogContent] = @BlogContent, ";
                parametersCount++;
            }
            if(conditions.Length == 0)
            {
                return NotFound("No valid data.");
            }
            conditions = conditions.Substring(0, conditions.Length - 2);

            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET {conditions}
 WHERE BlogId = @BlogId";

            AdoDotNetParameter[] parameters = new AdoDotNetParameter[parametersCount];
            parameters[0] = parameters[0] = new AdoDotNetParameter("@BlogId", id);
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                parameters[1] = new AdoDotNetParameter("@BlogTitle", blog.BlogTitle);
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                parameters[2] = new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor);
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                parameters[3] = new AdoDotNetParameter("@BlogContent", blog.BlogContent);
            }
            int result = _adoDotNetService.Execute(query, parameters);
            string message = result > 0 ? "Patching Successful" : "Patching Failed";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var item = FindById(id);
            if(item is null)
            {
                return NotFound("No data found.");
            }

            string query = "delete from tbl_blog where BlogId = @BlogId";
            AdoDotNetParameter[] parameters = new AdoDotNetParameter[1];
            parameters[0] = new AdoDotNetParameter("@BlogId", id);
            int result = _adoDotNetService.Execute(query, parameters);

            string message = result > 0 ? "Deleting Successful" : "Deleting Failed";
            return Ok(message);
        }
        private BlogModel? FindById(int id)
        {
            string query = "select * from tbl_blog where BlogId = @BlogId";

            //AdoDotNetParameter[] parameters = new AdoDotNetParameter[1];
            //parameters[0] = new AdoDotNetParameter("@BlogId", id);
            //var list = _adoDotNetService.Query<BlogModel>(query, parameters);

            var item = _adoDotNetService.QueryFirstOrDefault<BlogModel>(query, new AdoDotNetParameter("@BlogId", id));

            return item;
        }
    }
}
