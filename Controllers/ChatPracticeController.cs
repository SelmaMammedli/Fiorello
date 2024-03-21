using Fiorello.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello.Controllers
{
    public class ChatPracticeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public ChatPracticeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            
        }
        public IActionResult Chat()
        {
            ViewBag.Users=_userManager.Users.ToList();
            return View();
        }
    }
}
