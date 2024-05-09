using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KSODotNetCore.RestApiWithNLayer.Features.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BL_Blog _blBlog;

        public BlogController()
        {
            _blBlog = new BL_Blog();
        }

        [HttpGet]
        public IActionResult Read()
        {
            var list = _blBlog.GetBlogs();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var item = _blBlog.GetBlog(id);
            if (item is null)
            {
                return NotFound("No data found.");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(BlogModel blog)
        {
            int result = _blBlog.CreateBlog(blog);
            string message = result > 0 ? "Saving Success." : "Saving Failed";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blog)
        {
            var item = _blBlog.GetBlog(id);
            if (item is null)
            {
                return NotFound("No data found.");
            }
            int result = _blBlog.UpdateBlog(id, blog);
            string message = result > 0 ? "Updating Success." : "Updating Failed.";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, BlogModel blog)
        {
            var item = _blBlog.GetBlog(id);
            if (item is null)
            {
                return NotFound("No data found.");
            }

            int result = _blBlog.PatchBlog(id, blog);
            string message = result > 0 ? "Updating Success." : "Updating Failed.";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _blBlog.GetBlog(id);
            if (item is null)
            {
                return NotFound("No data found.");
            }
            int result = _blBlog.DeleteBlog(id);
            string message = result > 0 ? "Deleting Success." : "Deleting Failed.";
            return Ok(message);
        }
    }
}
