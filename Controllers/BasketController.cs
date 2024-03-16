using Fiorello.DAL;
using Fiorello.Models;
using Fiorello.ViewModels.BasketVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Fiorello.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext _context;
        public BasketController(AppDbContext context)
        {
            _context = context;

        }

        public IActionResult AddBasket(int? id)
        {
            if (id == null) return BadRequest();
            var product = _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
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
                list = JsonConvert.DeserializeObject<List<BasketProductVM>>(basket);
            }
            var existProductInBasket = list.Find(p => p.Id == id);
            if (existProductInBasket is null)
            {
                BasketProductVM basketProductVM = new()
                {
                    Id = product.Id,
                    //Name = product.Name,
                    //Price = product.Price,
                    //CategoryName = product.Category.Name,
                    //MainImageUrl = product.ProductImages.Any(p => p.IsMain) ?
                    // product.ProductImages.FirstOrDefault(p => p.IsMain).ImageUrl :
                    // product.ProductImages.FirstOrDefault().ImageUrl,
                    BasketCount = 1
                };
                list.Add(basketProductVM);
            }
            else
            {
                existProductInBasket.BasketCount++;
            }

            var stringProduct = JsonConvert.SerializeObject(list);
            Response.Cookies.Append("basket", stringProduct);

            return RedirectToAction("Index", "Home");
        }
        public IActionResult ShowBasket()
        {
            var stringData = Request.Cookies["basket"];
            List<BasketProductVM> list;
            if (stringData is null)
            {
                list = new();
            }
            else
            {
                list = JsonConvert.DeserializeObject<List<BasketProductVM>>(stringData);
                foreach (var basketProduct in list)
                {
                    var existedProduct = _context.Products
                        .Include(p => p.ProductImages)
                        .Include(p => p.Category)
                        .FirstOrDefault(p => p.Id == basketProduct.Id);
                    if(existedProduct is not null)
                    {
                        basketProduct.Name = existedProduct.Name;
                        basketProduct.Price = existedProduct.Price;
                        basketProduct.MainImageUrl = existedProduct.ProductImages.Any(p => p.IsMain) ?
                      existedProduct.ProductImages.FirstOrDefault(p => p.IsMain).ImageUrl :
                      existedProduct.ProductImages.FirstOrDefault().ImageUrl;
                        basketProduct.CategoryName = existedProduct.Category.Name;

                    }
                }
            }

            return View(list);
        }
        public IActionResult Increase(int?id)
        {
            if (id == null) return BadRequest();
            var product = _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            List<BasketProductVM> list=new();
            var basket = Request.Cookies["basket"];
           
                list = JsonConvert.DeserializeObject<List<BasketProductVM>>(basket);
            
            var existProductInBasket = list.Find(p => p.Id == id);
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

            var stringProduct = JsonConvert.SerializeObject(list);
            Response.Cookies.Append("basket", stringProduct);

            return RedirectToAction("ShowBasket");

        }
        public IActionResult Decrease(int?id)
        {
            if (id == null) return BadRequest();
            var product = _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            List<BasketProductVM> list = new();
            var basket = Request.Cookies["basket"];

            list = JsonConvert.DeserializeObject<List<BasketProductVM>>(basket);

            var existProductInBasket = list.Find(p => p.Id == id);
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
                existProductInBasket.BasketCount--;
                if (existProductInBasket.BasketCount==0)
                {
                    var existProduct = list.FirstOrDefault(p => p.Id == id);
                    list.Remove(existProduct);
                }
            }

            var stringProduct = JsonConvert.SerializeObject(list);
            Response.Cookies.Append("basket", stringProduct);

            return RedirectToAction("ShowBasket");
        }
        public IActionResult Remove(int?id)
        {
            var stringData = Request.Cookies["basket"];
            List<BasketProductVM> list=new();
            
                list = JsonConvert.DeserializeObject<List<BasketProductVM>>(stringData);
            var existProduct=list.FirstOrDefault(p=> p.Id == id);
            list.Remove(existProduct);
            
                 var stringProduct = JsonConvert.SerializeObject(list);
            Response.Cookies.Append("basket", stringProduct);



            return RedirectToAction("ShowBasket");
        }
    }
}
