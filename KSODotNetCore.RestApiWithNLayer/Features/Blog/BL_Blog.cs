using Microsoft.EntityFrameworkCore;

namespace KSODotNetCore.RestApiWithNLayer.Features.Blog
{
    public class BL_Blog
    {
        private readonly DA_Blog _daBlog;

        public BL_Blog()
        {
            _daBlog = new DA_Blog();
        }

        public List<BlogModel> GetBlogs()
        {
            var list = _daBlog.GetBlogs();
            return list;
        }

        public BlogModel GetBlog(int id)
        {
            var item = _daBlog.GetBlog(id);
            return item;
        }

        public int CreateBlog(BlogModel requestModel)
        {
            int result = _daBlog.CreateBlog(requestModel);
            return result;
        }

        public int UpdateBlog(int id, BlogModel requestModel)
        {
            int result = _daBlog.UpdateBlog(id, requestModel);
            return result;
        }

        public int PatchBlog(int id, BlogModel requestModel)
        {
            int result = _daBlog.PatchBlog(id, requestModel);
            return result;
        }

        public int DeleteBlog(int id)
        {
            int result = _daBlog.DeleteBlog(id);
            return result;
        }
    }
}
