using Fiorello.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorello.ViewComponents
{
    public class ProductViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;
        public ProductViewComponent(AppDbContext context)
        {
            _context = context;
            
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = _context.Products
             .Include(p => p.Category)
             .Include(p => p.ProductImages)
             .ToList();
            return View(await Task.FromResult(products));

        }
    }
}
