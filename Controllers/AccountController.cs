using Fiorello.Enums;
using Fiorello.Models;
using Fiorello.ViewModels.UserVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Fiorello.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {

            if(!ModelState.IsValid) return View();
            AppUser user = new();
            user.FullName = registerVM.FullName;
            user.Email = registerVM.Email;
            user.UserName = registerVM.UserName;
            IdentityResult result= await _userManager.CreateAsync(user,registerVM.Password);
            if(!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(registerVM);

            }
            //await _userManager.AddToRoleAsync(user,Roles.Member.ToString());
            await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            //await _userManager.AddToRoleAsync(user, Roles.SuperAdmin.ToString());
            return RedirectToAction("Index","Home");

        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View();
            var user = await _userManager.FindByEmailAsync(loginVM.UserNameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(loginVM.UserNameOrEmail);
                if (user == null)
                {
                    ModelState.AddModelError("","Email or UserName or Password invalid!");
                    return View(loginVM);
                }
            }
            SignInResult result=  await _signInManager.PasswordSignInAsync(user,loginVM.Password,loginVM.RememberMe,true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email or UserName or Password invalid!");
                return View(loginVM);
            }
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> Logout()
        {
            await  _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> CreateRole()
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                await _roleManager.CreateAsync(new() { Name = role.ToString() });
            }
            //if (!await _roleManager.RoleExistsAsync("Admin"))
            //{
            //    await _roleManager.CreateAsync(new() { Name = "Admin" });
            //}
            //if(!await _roleManager.RoleExistsAsync("Member"))
            //{
            //    await _roleManager.CreateAsync(new() { Name = "Member" });
            //}
            //if(!await _roleManager.RoleExistsAsync("SuperAdmin"))
            //{
            //    await _roleManager.CreateAsync(new() { Name = "SuperAdmin" });
            //}

            return Content("Roles added");
        }
    }
}
