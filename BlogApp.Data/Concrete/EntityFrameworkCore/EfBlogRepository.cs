using BlogApp.DataAccess.Abstract;
using BlogApp.Entities.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BlogApp.DataAccess.Concrete.EntityFrameworkCore
{
    public class EfBlogRepository : IBlogRepository
    {
        private BlogContext _context;

        public EfBlogRepository(BlogContext context)
        {
            _context = context;
        }

        public IQueryable<Blog> GetAll()
        {
            return _context.Blogs;
        }

        public Blog GetById(int blogId)
        {
            return _context.Blogs.FirstOrDefault(b => b.BlogId == blogId);
        }

        public void AddBlog(Blog entity)
        {
            _context.Blogs.Add(entity);
            _context.SaveChanges();
        }

        public void UpdateBlog(Blog entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteBlog(int blogId)
        {
            var romeveBlog = _context.Blogs.FirstOrDefault(b => b.BlogId == blogId);
            if (romeveBlog != null)
            {
                _context.Blogs.Remove(romeveBlog);
                _context.SaveChanges();
            }
        }
    }
}
