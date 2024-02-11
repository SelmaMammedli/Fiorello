using Fiorello.DAL;
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
    }
}
