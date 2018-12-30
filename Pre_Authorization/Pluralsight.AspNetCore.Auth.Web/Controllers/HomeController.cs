using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pluralsight.AspNetCore.Auth.Web.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("userinformation")]
        [Authorize]
        public IActionResult UserInformation()
        {
            return View();
        }

        [Route("agelimited")]
        [Authorize(Policy= "AgeLimit")]
        public IActionResult AgeLimited()
        {
            return View();
        }

        [Route("borderaccess")]
        [Authorize(Policy = "BorderAccess")]
        public IActionResult BorderAccess()
        {
            return View();
        }
    }
}
