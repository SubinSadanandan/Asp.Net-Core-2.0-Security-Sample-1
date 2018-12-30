using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_SocialLogins.Services
{
    public interface IUserService
    {
        Task<User> GetUserbyId(string id);
        Task<User> AddUser(string id, string displayName, string email);
    }
}
