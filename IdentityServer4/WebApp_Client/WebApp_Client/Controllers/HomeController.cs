using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using WebApp_Client.Models;

namespace WebApp_Client.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            //var disco = await DiscoveryClient.GetAsync("https://localhost:44398/");
            //var tokenClient = new TokenClient(disco.TokenEndpoint, "WebApp", "My Secret");
            //var tokenResponse = await tokenClient.RequestClientCredentialsAsync("DemoApi");

            //object model;
            //if(tokenResponse.IsError)
            //{
            //    model = "Error...could not get access token for API";
            //}
            //else
            //{
            //    var client = new HttpClient();
            //    client.SetBearerToken(tokenResponse.AccessToken);
            //    var response = await client.GetAsync("https://localhost:44390/api/text/welcome");

            //    if(response.IsSuccessStatusCode)
            //    {
            //        model = await response.Content.ReadAsStringAsync();
            //    }
            //    else
            //    {
            //        model = "Error...could not retrieve text";
            //    }
            //}

            return View();
        }

        [Route("userinformation")]
        [Authorize]
        public IActionResult UserInformation()
        {
            return View();
        }

        [Route("spa")]
        public IActionResult spa()
        {
            return View();
        }

        [Route("callapi")]
        [Authorize]
        public async Task<IActionResult> CallApi()
        {
            string accessToken;
            try
            {
                accessToken = await GetAccessToken();
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.GetBaseException().Message;
                return View();
            }

            var client = new HttpClient();
            client.SetBearerToken(accessToken);

            try
            {
                var content = await client.GetStringAsync("https://localhost:44390/api/user");
                ViewBag.ApiResponse = content;
            }
            catch(Exception ex)
            {
                ViewBag.ApiResponse = ex.GetBaseException().Message;
            }

            ViewBag.AccessToken = accessToken;
            ViewBag.RefreshToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

            return View();
        }

        private async Task<string> GetAccessToken()
        {
            var exp = await HttpContext.GetTokenAsync("expires_at");
            var expires = DateTime.Parse(exp);

            if(expires > DateTime.Now)
            {
                return await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            }

            return await GetRefreshedAccessToken();
        }

        private async Task<string> GetRefreshedAccessToken()
        {
            var discoClient = await DiscoveryClient.GetAsync("https://localhost:44398/");
            var tokenClient = new TokenClient(discoClient.TokenEndpoint, "WebApp1", "MySecret");

            var refreshToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);
            var tokenResponse = await tokenClient.RequestRefreshTokenAsync(refreshToken);

            if(!tokenResponse.IsError)
            {
                var auth = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                auth.Properties.UpdateTokenValue(OpenIdConnectParameterNames.AccessToken, tokenResponse.AccessToken);
                auth.Properties.UpdateTokenValue(OpenIdConnectParameterNames.RefreshToken, tokenResponse.RefreshToken);
                var expiresAt = DateTime.UtcNow + TimeSpan.FromSeconds(tokenResponse.ExpiresIn);
                auth.Properties.UpdateTokenValue("expires_at", expiresAt.ToString("o", CultureInfo.InvariantCulture));

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    auth.Principal, auth.Properties);

                return tokenResponse.AccessToken;

            }

            throw tokenResponse.Exception;
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
