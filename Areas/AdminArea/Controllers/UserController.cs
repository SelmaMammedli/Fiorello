using Fiorello.Areas.ViewModels.User;
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
                    .Where(u=>u.IsActive)
                    .AsNoTracking()
                    .ToList());
            else
                return View(_userManager.Users
                    .AsNoTracking()
                    .Where(u=>u.UserName.ToLower().Contains(search.ToLower())&&u.IsActive).ToList());

        }
        public IActionResult DeletedUser(string search)
        {


            if (search is null)
                return View(_userManager.Users
                    .Where(u=>!u.IsActive)
                    .AsNoTracking()
                    .ToList());
            else
                return View(_userManager.Users
                    .AsNoTracking()
                    .Where(u => u.UserName.ToLower().Contains(search.ToLower())&&!u.IsActive).ToList());

        }
        public async Task<IActionResult> Detail(string id)
        {
            if(id == null)return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if(user == null)return NotFound();
            var roles = await _userManager.GetRolesAsync(user);
            UserDetailVM userDetailVM = new();
            userDetailVM.User = user;
            userDetailVM.UserRoles = roles;
            return View(userDetailVM);
        }
        public async Task<IActionResult> IsActive(string id)
        {
            if (id == null) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            user.IsActive=!user.IsActive;
            await _userManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }
    }
}
