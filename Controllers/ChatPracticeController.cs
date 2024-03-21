using Microsoft.AspNetCore.Mvc;

namespace Fiorello.Controllers
{
    public class ChatPracticeController : Controller
    {
        public IActionResult Chat()
        {
            return View();
        }
    }
}
