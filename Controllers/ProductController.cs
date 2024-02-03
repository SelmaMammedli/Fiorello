using Fiorello.DAL;
using Fiorello.ViewModels.ProductVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorello.Controllers
{
    public class ProductController : Controller
    {
        public readonly AppDbContext _appDbContext;
        private static int Skip=4;

        public ProductController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            var products = _appDbContext.Products
                .Include(p=>p.Category)
                .Include(p=>p.ProductImages)
                .OrderBy(p => p.Id)
                .Take(4)
                .ToList();
            Skip = 4;
            return View(products);
        }
        public IActionResult LoadMore()
        {
            var product = _appDbContext.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .Skip(Skip)
                .Take(4)
                //.Select(p => new ProductReturnVM
                //{
                //    Id = p.Id,
                //    Name = p.Name,
                //    Price = p.Price,
                //    CategoryName=p.Category.Name,
                //    ImageUrl=p.ProductImages.FirstOrDefault(pm=>pm.IsMain).ImageUrl

                //})
                .ToList();
            Skip+= 4;
            return PartialView("_LoadMorePartial",product);
        }
    }
}
