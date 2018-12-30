using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pluralsight.AspNetCore.Auth.Web.Security
{
    public class BorderAccessRequirement: IAuthorizationRequirement
    {
        public string Country { get; }
        public BorderAccessRequirement(string country)
        {
            Country = country;
        }
    }
}
