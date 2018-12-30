using Microsoft.AspNetCore.Authorization;
using Pluralsight.AspNetCore.Auth.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pluralsight.AspNetCore.Auth.Web.Security
{
    public class VisaHolderHandler : AuthorizationHandler<BorderAccessRequirement>
    {
        private IVisaService _visas;
        public VisaHolderHandler(IVisaService visas)
        {
            _visas = visas;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context
            , BorderAccessRequirement requirement)
        {
            var hasVisa = await _visas.UserHasVisaForCountryAsync(context.User.Identity.Name, requirement.Country);
            if(hasVisa)
            {
                context.Succeed(requirement);
            }
        }
    }
}
