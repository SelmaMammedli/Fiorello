using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole>roleManager)
        {
            _roleManager = roleManager;
            
        }
        public IActionResult Index()
        {
            return View(_roleManager.Roles.ToList());
        }
    }
}
