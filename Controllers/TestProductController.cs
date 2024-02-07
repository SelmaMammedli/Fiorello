using Fiorello.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorello.Controllers
{
    public class TestProductController : Controller
    {
        public readonly AppDbContext _appDbContext;

        public TestProductController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            return View(_appDbContext.Products
             .Include(p=>p.Category)
             .Include(p => p.ProductImages)
             .ToList());
        }
    }
}
