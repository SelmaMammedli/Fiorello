﻿using Fiorello.DAL;
using Fiorello.Models;
using Fiorello.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Fiorello.Controllers
{
    public class HomeController : Controller
    {
        public readonly AppDbContext _appDbContext;

        public HomeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            HomeVm homeVm = new HomeVm();
            homeVm.Sliders=_appDbContext.Sliders.ToList();
            homeVm.SliderContent = _appDbContext.SliderContent.FirstOrDefault();
            homeVm.Category=_appDbContext.Category.ToList();
            homeVm.Products = _appDbContext.Products
             .Include(p=>p.Images)
             .ToList();
            return View(homeVm);
        }

       
    }
}