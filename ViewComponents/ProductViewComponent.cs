using Fiorello.DAL;
using Fiorello.Models;
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
        public async Task<IViewComponentResult> InvokeAsync(int take)
        {
            List<Product> products;

            if (take == 0)
            {
                products = _context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductImages)
            .ToList();

            }
            else
            {
                products = _context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductImages)
            .Take(take)
            .ToList();
            }

            
            return View(await Task.FromResult(products));

        }
    }
}
