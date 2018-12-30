using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_SocialLogins.Services
{
    public class DummyUserService : IUserService
    {
        private IDictionary<string, User> _users = new Dictionary<string, User>();

        public Task<User> AddUser(string id, string displayName, string email)
        {
            var user = User.Create(id, displayName, email);
            _users.Add(id,user);
            return Task.FromResult(user);
        }

        public Task<User> GetUserbyId(string id)
        {
            if(_users.ContainsKey(id))
            {
                return Task.FromResult(_users[id]);
            }

            return Task.FromResult<User>(null);
        }
    }
}
