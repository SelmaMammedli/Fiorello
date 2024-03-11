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
    }
}
