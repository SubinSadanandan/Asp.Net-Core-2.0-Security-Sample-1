using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pluralsight.AspNetCore.Auth.Web.Services
{
    public interface IUserService
    {
        Task<bool> ValidateCredentials(string username, string password, out User user);
    }

    public class User
    {
        public User(string username, string dateOfBirth)
        {
            Username = username;
            this.DateOfBirth = dateOfBirth;
        }

        public string Username { get; }
        public string DateOfBirth { get; }
    }
}
