using Fiorello.DAL;
using Fiorello.ViewModels.ProductVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Fiorello.Controllers
{
    public class ProductController : Controller
    {
        public readonly AppDbContext _appDbContext;
       

        public ProductController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
           var query= _appDbContext.Products.AsQueryable();
           ViewBag.ProductCount=query.Count();
            var products = query
                .Include(p=>p.Category)
                .Include(p=>p.ProductImages)
                .OrderBy(p => p.Id)
                .Take(4)
                .ToList();
            
            return View(products);
        }
        public IActionResult LoadMore(int skip)
        {
           
            var product = _appDbContext.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .Skip(skip)
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
           
            return PartialView("_LoadMorePartial",product);
        }
        public IActionResult Search(string input)
        {
            var products = _appDbContext.Products
               //.Where(p => p.Name.ToLower() == input.ToLower())
               // .Where(p => p.Name.Equals(input,StringComparison.OrdinalIgnoreCase))
                .Include(p=>p.ProductImages)
                .Where(p => p.Name.Contains(input))
                .OrderByDescending(p => p.Id)
                .Take(5)
                .ToList();
            return PartialView("_SearchPartial",products);
        }
        public IActionResult Detail(int? id)
        {
            var products = _appDbContext.Products;
            return View(products);

        }
    }
}
