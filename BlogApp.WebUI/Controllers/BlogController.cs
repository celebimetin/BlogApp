using BlogApp.DataAccess.Abstract;
using BlogApp.Entities.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.WebUI.Controllers
{
    public class BlogController : Controller
    {
        private IBlogRepository _blogRepository;
        private ICategoryRepository _categoryRepository;

        public BlogController(IBlogRepository blogRepository, ICategoryRepository categoryRepository)
        {
            _blogRepository = blogRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index(int? id, string q)
        {
            var query = _blogRepository.GetAll().Where(x => x.isApproved);
            if (id != null)
                query = query.Where(x => x.CategoryId == id);
            if (!String.IsNullOrEmpty(q))
                query = query.Where(x => x.Title.Contains(q) || x.Description.Contains(q) || x.Body.Contains(q));
            return View(query.OrderByDescending(x => x.Date));
        }

        public IActionResult List()
        {
            return View(_blogRepository.GetAll());
        }

        public IActionResult Details(int id)
        {
            return View(_blogRepository.GetById(id));
        }

        [HttpGet]
        public IActionResult Create()
        {
            GetCategoryListMethod();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Blog entity, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                entity.Image = file.FileName;
                _blogRepository.AddBlog(entity);
                return RedirectToAction("List");
            }
            GetCategoryListMethod();
            return View(entity);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            GetCategoryListMethod();
            return View(_blogRepository.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Blog entity, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        entity.Image = file.FileName;
                    } 
                }
                _blogRepository.UpdateBlog(entity);
                TempData["message"] = $"{entity.Title} güncellendi.";
                return RedirectToAction("List");
            }
            GetCategoryListMethod();
            return View(entity);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(_blogRepository.GetById(id));
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int BlogId)
        {
            _blogRepository.DeleteBlog(BlogId);
            return RedirectToAction("List");
        }

        private void GetCategoryListMethod()
        {
            ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "CategoryId", "CategoryName");
        }
    }
}
