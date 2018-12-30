using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Test.AspNetCore.Auth.Api.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class ApiController : Controller
    {
        [Route("text/welcome")]
        [Authorize]
        public IActionResult GetWelcomeText()
        {
            return Content("Welcome " + User.Identity.Name);
        }

        [Route("user")]
        [Authorize]
        public IActionResult GetUser()
        {
            return Content("User: " + User.Identity.Name);
        }

        [Route("user1")]
        [Authorize]
        public IActionResult GetUserInformation()
        {
            return Ok(new
            {
                id = User.FindFirst("sub").Value,
                username = User.Identity.Name
            });
        }

    }
}
