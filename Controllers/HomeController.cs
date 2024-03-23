using Fiorello.DAL;
using Fiorello.Hubs;
using Fiorello.Models;
using Fiorello.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Fiorello.Controllers
{
    public class HomeController : Controller
    {
        public readonly AppDbContext _appDbContext;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(AppDbContext appDbContext, IHubContext<ChatHub> hubContext,UserManager<AppUser> userManager)
        {
            _appDbContext = appDbContext;
            _hubContext = hubContext;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
           // var images=_appDbContext.ProductImages.ToList();
            HomeVm homeVm = new HomeVm();
            homeVm.Sliders=_appDbContext.Sliders.ToList();
            homeVm.SliderContent = _appDbContext.SliderContent.FirstOrDefault();
            homeVm.Category=_appDbContext.Category.ToList();
            homeVm.Products = _appDbContext.Products
             .Include(p=>p.ProductImages)
             .ToList();
            return View(homeVm);
        }
        public IActionResult Detail(int? id)
        {
            var products = _appDbContext.Products
                .Include(p => p.ProductImages)
                .Include(p => p.Category)
                .ToList();
            if (id == null) return BadRequest();
            if (products.Exists(p => p.Id == id))
            {
                return View(products.Find(p => p.Id == id));
            }

            return BadRequest();

        }
        public async Task<IActionResult> ShowAlert(string userId)
        {
            
            var user=await _userManager.FindByIdAsync(userId);
            if (user.ConnectionId is null) return NotFound();
            await _hubContext.Clients.Client(user.ConnectionId).SendAsync("UserShowAlert", user.FullName);
            return RedirectToAction("chat", "chatpractice");
        }


    }
}