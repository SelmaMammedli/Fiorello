using Fiorello.DAL;
using Fiorello.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorello.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        public UserController(UserManager<AppUser>userManager,AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
            
        }

        public IActionResult Index(string search)
        {
            
                    
            if (search is null)
                return View(_userManager.Users
                    .AsNoTracking()
                    .ToList());
            else
                return View(_userManager.Users
                    .AsNoTracking()
                    .Where(u=>u.UserName.ToLower().Contains(search.ToLower())).ToList());

        }
    }
}
