﻿using Fiorello.Enums;
using Fiorello.Models;
using Fiorello.ViewModels.UserVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
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
            user.IsActive = true;
            IdentityResult result= await _userManager.CreateAsync(user,registerVM.Password);
            if(!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(registerVM);

            }
            await _userManager.AddToRoleAsync(user,Roles.Member.ToString());
            //await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            //await _userManager.AddToRoleAsync(user, Roles.SuperAdmin.ToString());


            //email confirmation
            var token=await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var link = Url.Action(nameof(VerifyEmail), "Account", new { token, email = user.Email },
                Request.Scheme, Request.Host.ToString());

            MailMessage mailMessage = new();
            mailMessage.From=new MailAddress("7fvqmgj@code.edu.az","Verify Fiorello email");
            mailMessage.To.Add(new MailAddress(user.Email));
            mailMessage.Subject = "Verify Email";
            mailMessage.IsBodyHtml = true;
            string body = String.Empty;
            using(StreamReader streamReader=new StreamReader("wwwroot/emailTemplate/verifyEmailTemplate.html"))
            {
                body= streamReader.ReadToEnd();
            }
            body=body.Replace("{{name}}", user.FullName);
            body=body.Replace("{{link}}", link);
            mailMessage.Body = body;
           

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;

            smtpClient.Credentials = new NetworkCredential("7fvqmgj@code.edu.az", "vunl xkhb nqee iwtz");
            smtpClient.Send(mailMessage);

            return RedirectToAction("Index","Home");

        }
        public async Task<IActionResult> VerifyEmail(string token,string email)
        {
            AppUser user=await _userManager.FindByEmailAsync(email);
            await _userManager.ConfirmEmailAsync(user,token);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM,string ? ReturnUrl)
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
            if(result.IsLockedOut)
            {
                ModelState.AddModelError("", "Blocked!");
                return View(loginVM);
            }
            if (!user.EmailConfirmed)
            {
                ModelState.AddModelError("", "Emaili verify et...!");
                return View(loginVM);
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email or UserName or Password invalid!");
                return View(loginVM);
            }
            if (!user.IsActive)
            {
                ModelState.AddModelError("", "Your account is blocked!");
                return View(loginVM);
            }
            if (ReturnUrl is not null)
            {
                return Redirect(ReturnUrl);
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
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            AppUser user=await _userManager.FindByEmailAsync(email);
            if(user is null)
            {
                ModelState.AddModelError("Email","Email movcud deyil");
                return View();
            }

            //send email part
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email },
                Request.Scheme, Request.Host.ToString());

            MailMessage mailMessage = new();
            mailMessage.From = new MailAddress("7fvqmgj@code.edu.az", "Email for reset password");
            mailMessage.To.Add(new MailAddress(user.Email));
            mailMessage.Subject = "Reset Password";
            mailMessage.Body = $"<a href={link}>Please click here to reset password</a>";
            mailMessage.IsBodyHtml = true;
           
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;

            smtpClient.Credentials = new NetworkCredential("7fvqmgj@code.edu.az", "nahq rjvx xbbc jaqo");
            smtpClient.Send(mailMessage);

            return RedirectToAction("Index", "Home");
        }
        public IActionResult ResetPassword()
        {
            return View();
        }
    }
}
