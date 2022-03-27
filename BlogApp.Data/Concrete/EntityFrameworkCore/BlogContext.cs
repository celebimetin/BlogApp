using BlogApp.Entities.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.DataAccess.Concrete.EntityFrameworkCore
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {

        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}