using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebApp_SocialLogins.Models;
using WebApp_SocialLogins.Services;

namespace WebApp_SocialLogins.Controllers
{
    [Route("Auth")]
    public class AuthController : Controller
    {
        private IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("Signin")]
        public async Task<IActionResult> SignIn()
        {
            var authResult = await HttpContext.AuthenticateAsync("Temporary");
            if(authResult.Succeeded)
            {
                return RedirectToAction("Profile");
            }

            //return Challenge(new AuthenticationProperties { RedirectUri = "/" });
            return View();
        }

        [Route("Signin/{provider}")]
        public IActionResult SignIn(string provider,string returnurl = null)
        {
            var redirectUri = Url.Action("Profile");
            if(returnurl != null)
            {
                redirectUri += "?returnUrl=" + returnurl;
            }

            return Challenge(new AuthenticationProperties { RedirectUri = redirectUri },provider);
        }

        [Route("signin/profile")]
        public async Task<IActionResult> Profile(string returnUrl = null)
        {
            //Since user got authenticated with cookies scheme so User proprty will not be there,
            //so we need to authenticate with temporary scheme
            var authResult = await HttpContext.AuthenticateAsync("Temporary");

            if(!authResult.Succeeded)
            {
                return RedirectToAction("SignIn");
            }

            var user = await _userService.GetUserbyId
                (authResult.Principal.FindFirst(ClaimTypes.NameIdentifier).Value);

            if(user !=null)
            {
                return await SignInUser(user, returnUrl);
            }

            var model = new ProfileModel
            {
                DisplayName = authResult.Principal.Identity.Name
            };
            var emailClaim = authResult.Principal.FindFirst(ClaimTypes.Email);
            if(emailClaim != null)
            {
                model.Email = emailClaim.Value;
            }

            return View(model);
        }

        [Route("signin/profile")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileModel model,string returnUrl = null)
        {
            var authResult = await HttpContext.AuthenticateAsync("Temporary");
            if(!authResult.Succeeded)
            {
                return RedirectToAction("SignIn");
            }

            if(ModelState.IsValid)
            {
                var user = await _userService.AddUser
                    (authResult.Principal.FindFirst(ClaimTypes.NameIdentifier).Value, model.DisplayName,model.Email);

                return await SignInUser(user, returnUrl);
            }

            return View(model);

        }

        private async Task<IActionResult> SignInUser(User user,string returnUrl = null)
        {
            await HttpContext.SignOutAsync("Temporary");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.DisplayName),
                new Claim(ClaimTypes.Email,user.Email)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return Redirect(returnUrl == null ? "/" : returnUrl);

        }


        [Route("signout")]
        [HttpPost]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}