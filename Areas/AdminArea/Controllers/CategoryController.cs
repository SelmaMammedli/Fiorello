using Fiorello.DAL;
using Fiorello.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
            
        }
        public IActionResult Index()
        {
            return View(_context.Category.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(string name,string desc)
        {
            Category category = new();
            category.Name = name;
            category.Description = desc;
            _context.Category.Add(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
