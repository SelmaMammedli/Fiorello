using Azure.Core;
using Fiorello.DAL;
using Fiorello.Services.Interfaces;
using Fiorello.ViewModels.BasketVM;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Fiorello.Services.Implementations
{
    public class BasketService : IBasketService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly AppDbContext _context;
        public BasketService(IHttpContextAccessor contextAccessor, AppDbContext context)
        {
            _contextAccessor = contextAccessor;
            _context = context;
            
        }
        public int BasketCount()
        {
            var basket =_contextAccessor.HttpContext.Request.Cookies["basket"];
            if (basket == null) return 0;
            var products = JsonConvert.DeserializeObject<List<BasketProductVM>>(basket);
            return products.Count;
            
           
        }

        public List<BasketProductVM> GetProducts()
        {
            var basket = _contextAccessor.HttpContext.Request.Cookies["basket"];
            
            if(basket is null) return new List<BasketProductVM> ();
            var list= JsonConvert.DeserializeObject<List<BasketProductVM>>(basket);
            foreach (var basketProduct in list)
            {
                var existedProduct = _context.Products
                    .Include(p=>p.ProductImages)
                    .Include(p=>p.Category)
                    .FirstOrDefault(p => p.Id == basketProduct.Id);
                if (existedProduct is not null)
                {
                    basketProduct.Name = existedProduct.Name;
                    basketProduct.Price = existedProduct.Price;
                    basketProduct.MainImageUrl = existedProduct.ProductImages.Any(p => p.IsMain) ?
                  existedProduct.ProductImages.FirstOrDefault(p => p.IsMain).ImageUrl :
                  existedProduct.ProductImages.FirstOrDefault().ImageUrl;
                }
            }
            return list;
            
        }
    }
}
