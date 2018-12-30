using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebApp1.Models;
using WebApp1.Services;

namespace WebApp1.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        private IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("signin")]
        public IActionResult SignIn()
        {
            return View(new SignInModel());
        }

        [Route("signin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInModel model,string returnUrl1 = null)
        {
            if(ModelState.IsValid)
            {
                User user;
                if(await _userService.ValidateCredentials(model.Username,model.Password,out user))
                {
                    await SignInUser(user.Username);
                    if(returnUrl1 != null)
                    {
                        return Redirect(returnUrl1);
                    }
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        [Route("signup")]
        public IActionResult SignUp()
        {
            return View(new SignUpModel());
        }

        [Route("signup")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            if(ModelState.IsValid)
            {
                if(await _userService.AddUser(model.Username,model.Password))
                {
                    await SignInUser(model.Username);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("Error", "Could not add user. Username already in use...");
            }

            return View(model);
        }

        [Route("signout")]
        [HttpPost]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public async Task SignInUser(string username)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,username),
                new Claim("name",username)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);
        }
    }
}