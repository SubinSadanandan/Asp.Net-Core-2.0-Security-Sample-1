using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApp1.Services
{
    public class ProfileClaimsTransformation : IClaimsTransformation
    {
        private IProfileService _profileService;
        public ProfileClaimsTransformation(IProfileService profileService)
        {
            _profileService = profileService;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var identity = principal.Identities.FirstOrDefault(x => x.IsAuthenticated);
            if(identity == null)
            {
                return principal;
            }

            var idClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
            var profile = await _profileService.GetUserProfileASync(idClaim.Value);
            if(profile == null)
            {
                return principal;
            }

            var claims = new List<Claim>
            {
                idClaim,
                new Claim(ClaimTypes.GivenName,profile.FirstName,ClaimValueTypes.String,"ProfileClaimTransformation"),
                new Claim(ClaimTypes.Surname,profile.LastName,ClaimValueTypes.String,"ProfileClaimTransformation"),
                new Claim(ClaimTypes.Name,profile.FirstName + " " +profile.LastName,ClaimValueTypes.String,"ProfileClaimTransformation"),
            };
            claims.AddRange(profile.Roles.Select(x => new Claim(ClaimTypes.Role, x, 
                ClaimValueTypes.String, "ProfileClaimTransformation")));

            var newIdentity = new ClaimsIdentity(claims, identity.AuthenticationType);
            return new ClaimsPrincipal(newIdentity);
        }
    }
}
