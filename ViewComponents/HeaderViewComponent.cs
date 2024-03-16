using Fiorello.DAL;
using Fiorello.Models;
using Fiorello.ViewModels.BasketVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Fiorello.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public HeaderViewComponent(AppDbContext context,UserManager<AppUser>userManager)
        {
            _context = context;
            _userManager = userManager;

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var basket = Request.Cookies["basket"];
            ViewBag.ProductsCount = 0;
            if (basket is not null)
            {
                var products = JsonConvert.DeserializeObject<List<BasketProductVM>>(basket);
                ViewBag.ProductsCount = products.Sum(p => p.BasketCount);
            }

            var bios = _context.Bios
                .ToDictionary(b=>b.Key,b=>b.Value);
            ViewBag.UserFullName = "";
            if (User.Identity.IsAuthenticated)
            {
                var user =await _userManager.FindByNameAsync(User.Identity.Name);
                ViewBag.UserFullName = user.FullName;//bu headerde adminin ve ya memberin adinin yazilmasidi user.Email yazaraq emailin gore bilerik
            }
            



            return View(await Task.FromResult(bios));

        }
    }
}
