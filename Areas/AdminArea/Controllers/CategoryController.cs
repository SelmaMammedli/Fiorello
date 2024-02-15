using Fiorello.Areas.ViewModels;
using Fiorello.Areas.ViewModels.Category;
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
        public IActionResult Create(CategoryCreateVM category)
        {
            if (!ModelState.IsValid) return View();
            if(_context.Category.Any(c=>c.Name.ToLower() == category.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "Bu adda kateqorya movcuddur");
                return View();

            }
            Category newCategory = new();
            newCategory.Name = category.Name;
            newCategory.Description = category.Description;
            _context.Category.Add(newCategory);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var existCategory=_context.Category.FirstOrDefault(s=>s.Id==id);
            if(existCategory is null) return NotFound();
            _context.Category.Remove(existCategory);

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int? id)
        {
            var category = _context.Category
                .ToList();
            if (id == null) return BadRequest();
            if (category.Exists(p => p.Id == id))
            {
                return View(category.Find(p => p.Id == id));
            }

            return BadRequest();

        }


    }
}
