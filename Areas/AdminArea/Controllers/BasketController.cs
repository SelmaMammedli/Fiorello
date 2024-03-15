using Microsoft.AspNetCore.Mvc;

namespace Fiorello.Areas.AdminArea.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SetData()
        {
            HttpContext.Response.Cookies.Append("name","filankes",new CookieOptions { MaxAge=TimeSpan.FromMinutes(10)});
            return Content("Set olundu");
        }
        public IActionResult GetData()
        {
            var data = Request.Cookies["name"];
            return Content(data); 
        }
    }
}
