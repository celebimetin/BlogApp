using BlogApp.Entities.Entity;
using System.Linq;

namespace BlogApp.DataAccess.Abstract
{
    public interface IBlogRepository
    {
        IQueryable<Blog> GetAll();
        Blog GetById(int blogId);
        void AddBlog(Blog entity);
        void UpdateBlog(Blog entity);
        void DeleteBlog(int blogId);
    }
}