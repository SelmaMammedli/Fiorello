using Fiorello.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorello.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var datas=_context.Products
                .Include(c=>c.Category)
                .Include(c=>c.ProductImages)
                .AsNoTracking()
                .ToList();
            return View(datas);
        }
    }
}
