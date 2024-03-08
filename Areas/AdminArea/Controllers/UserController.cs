using Fiorello.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public UserController(UserManager<AppUser>userManager)
        {
            _userManager = userManager;
            
        }

        public IActionResult Index()
        {
            return View(_userManager.Users.ToList());
        }
    }
}
