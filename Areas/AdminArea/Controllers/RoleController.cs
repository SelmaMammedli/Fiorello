using Fiorello.Areas.ViewModels.User;
using Fiorello.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        public RoleController(RoleManager<IdentityRole>roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            
        }
        public IActionResult Index()
        {
            return View(_roleManager.Roles.ToList());
        }
        public async Task<IActionResult> Create(string role)
        {
            if(string.IsNullOrEmpty(role)) return BadRequest("input bos ola bilmez");
            var result=await _roleManager.CreateAsync(new IdentityRole { Name= role });
            if (!result.Succeeded) BadRequest("input bos ola bilmez");
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(string id)
        {
            var role=await _roleManager.FindByIdAsync(id);
            if (role is null) return NotFound();
            await _roleManager.DeleteAsync(role);
            return RedirectToAction("Index");
            
        }
        public async Task<IActionResult> EditUserRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null) return NotFound();
            var roles=_roleManager.Roles.ToList();
            var userRoles = await _userManager.GetRolesAsync(user);
            ChangeRoleVM changeRoleVM = new();
            changeRoleVM.Roles= roles;
            changeRoleVM.User = user;
            changeRoleVM.UserRoles= userRoles;
            return View(changeRoleVM);
        }
    }
}
