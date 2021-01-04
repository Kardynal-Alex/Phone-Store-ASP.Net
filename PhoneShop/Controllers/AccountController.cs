using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using PhoneShop.Models.DataModel;
using Microsoft.AspNetCore.Identity;
using PhoneShop.Models.AccountModel;
namespace PhoneShop.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationContext context;

        public AccountController(ApplicationContext ctx)
        {
            context = ctx;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if(ModelState.IsValid)
            {
                User user = context.Users.FirstOrDefault(x => x.Email == model.Email);
                if (user == null)
                {
                    user = new User { Email = model.Email, Password = model.Password };
                    Role userRole = context.Roles.FirstOrDefault(x => x.Name == "user");
                    if (userRole != null)
                    {
                        user.Role = userRole;
                    }
                    context.Users.Add(user);
                    await context.SaveChangesAsync();
                    await Authenticate(user);
                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("Email", "Incorect Login or Password");
            }
            return View(model);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                User user = context.Users.FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);
                if(user!=null)
                {
                    Role role = context.Roles.FirstOrDefault(x => x.Id == user.RoleId);
                    await Authenticate(user);
                    return RedirectToAction("Index", "Home");
                }else
                    ModelState.AddModelError("Email", "Incorect Login or Password");
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
