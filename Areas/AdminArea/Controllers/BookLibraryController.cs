using Microsoft.AspNetCore.Mvc;

namespace Fiorello.Areas.AdminArea.Controllers
{
    public class BookLibraryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
