using BlogApp.DataAccess.Abstract;
using BlogApp.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BlogApp.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IBlogRepository _blogRepository;

        public HomeController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public IActionResult Index()
        {
            var model = new HomeBlogModel();
            model.HomeBlogs = _blogRepository.GetAll().Where(x => x.isApproved && x.isHome).ToList();
            model.SliderBlogs = _blogRepository.GetAll().Where(x => x.isApproved && x.isSlider).ToList();
            return View(model);
        }

        public IActionResult List()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}