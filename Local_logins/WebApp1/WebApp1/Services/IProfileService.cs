using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp1.Services
{
    public interface IProfileService
    {
        Task<UserProfile> GetUserProfileASync(string userId);
    }

    public class UserProfile
    {
        public UserProfile(string firstName,string lastName,string[] roles)
        {
            FirstName = firstName;
            LastName = lastName;
            Roles = roles;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string[] Roles { get; set; }
    }
}
