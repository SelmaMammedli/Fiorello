using Fiorello.DAL;
using Fiorello.ViewModels.BasketVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Newtonsoft.Json;


namespace Fiorello.Areas.AdminArea.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext _context;
        public BasketController(AppDbContext context)
        {
            _context = context;
            
        }

        public IActionResult AddBasket(int?id)
        {
            if (id == null) return BadRequest();
            var product=_context.Products
                .Include(p=>p.Category)
                .Include(p=>p.ProductImages)
                .FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            List<BasketProductVM> list;
            var basket = Request.Cookies["basket"];
            if (basket is null)
            {
                list = new();
            }
            else
            {
                list=JsonConvert.DeserializeObject<List<BasketProductVM>>(basket);
            }
            var existProductInBasket=list.Find(p => p.Id == id);
            if (existProductInBasket is null)
            {
                BasketProductVM basketProductVM = new()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    CategoryName = product.Category.Name,
                    MainImageUrl = product.ProductImages.Any(p => p.IsMain) ?
                     product.ProductImages.FirstOrDefault(p => p.IsMain).ImageUrl :
                     product.ProductImages.FirstOrDefault().ImageUrl,
                    BasketCount = 1
                };
                list.Add(basketProductVM);
            }
            else
            {
                existProductInBasket.BasketCount++;
            }
            
            var stringProduct=JsonConvert.SerializeObject(list);
            Response.Cookies.Append("basket", stringProduct);
           
            return RedirectToAction("Index","Home");
        }
        public IActionResult ShowBasket()
        {
            var stringData = Request.Cookies["basket"];
            List<BasketProductVM> list ;
            if(stringData is null)
            {
                list = new();
            }
            else
            {
                list = JsonConvert.DeserializeObject<List<BasketProductVM>>(stringData);
            }
         
            return View(list); 
        }
        
    }
}
