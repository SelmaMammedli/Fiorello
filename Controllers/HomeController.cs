using Fiorello.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Fiorello.Controllers
{
    public class HomeController : Controller
    {
       

      

        public IActionResult Index()
        {
            return View();
        }

       
    }
}