using Fiorello.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class BookLibraryController : Controller
    {
        private readonly AppDbContext _context;
        public BookLibraryController(AppDbContext context)
        {
            _context = context;
            
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
