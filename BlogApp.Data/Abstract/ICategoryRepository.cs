using BlogApp.Entities.Entity;
using System.Linq;

namespace BlogApp.DataAccess.Abstract
{
    public interface ICategoryRepository
    {
        IQueryable<Category> GetAll();
        Category GetById(int categoryId);
        void AddCategory(Category entity);
        void UpdateCategory(Category entity);
        void DeleteCategory(int categoryId);
    }
}