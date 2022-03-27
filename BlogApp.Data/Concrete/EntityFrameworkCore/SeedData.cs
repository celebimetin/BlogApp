using BlogApp.Entities.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System;

namespace BlogApp.DataAccess.Concrete.EntityFrameworkCore
{
    public static class SeedData
    {
        public static void Seed(IApplicationBuilder app)
        {
            BlogContext context = app.ApplicationServices.GetRequiredService<BlogContext>();

            context.Database.Migrate();

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category
                    {
                        CategoryName = "Category 1",
                    },
                    new Category
                    {
                        CategoryName = "Category 2",
                    });

                context.SaveChanges();
            }
            if (!context.Blogs.Any())
            {
                context.Blogs.AddRange(
                    new Blog
                    {
                        Title = "Blog title 1",
                        Description = "Blog Description 1",
                        Body = "Blog body 1",
                        Date = DateTime.Now.AddDays(-1),
                        isApproved = true,
                        Image = "1.jpg",
                        CategoryId = 1
                    },
                    new Blog
                    {
                        Title = "Blog title 2",
                        Description = "Blog Description 2",
                        Body = "Blog body 2",
                        Date = DateTime.Now.AddDays(-2),
                        isApproved = true,
                        Image = "2.jpg",
                        CategoryId = 2
                    });

                context.SaveChanges();
            }
        }
    }
}