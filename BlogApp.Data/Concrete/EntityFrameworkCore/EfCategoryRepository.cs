using BlogApp.DataAccess.Abstract;
using BlogApp.Entities.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BlogApp.DataAccess.Concrete.EntityFrameworkCore
{
    public class EfCategoryRepository : ICategoryRepository
    {
        private BlogContext _context;

        public EfCategoryRepository(BlogContext context)
        {
            _context = context;
        }

        public IQueryable<Category> GetAll()
        {
            return _context.Categories;
        }

        public Category GetById(int categoryId)
        {
            return _context.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
        }

        public void AddCategory(Category entity)
        {
            _context.Categories.Add(entity);
            _context.SaveChanges();
        }

        public void UpdateCategory(Category entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteCategory(int categoryId)
        {
            var romeveCategory = _context.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (romeveCategory != null)
            {
                _context.Categories.Remove(romeveCategory);
                _context.SaveChanges();
            }
        }
    }
}
