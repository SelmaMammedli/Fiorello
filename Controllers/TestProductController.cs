using Microsoft.AspNetCore.Mvc;

namespace Fiorello.Controllers
{
    public class TestProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
