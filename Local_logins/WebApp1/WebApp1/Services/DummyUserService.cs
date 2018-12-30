using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp1.Services
{
    public class DummyUserService : IUserService
    {
        private IDictionary<string, (string passwordHash, User user)> _users =
            new Dictionary<string, (string passwordHash, User user)>();

        public DummyUserService(IDictionary<string,string> users)
        {
            foreach (var user in users)
            {
                _users.Add(user.Key.ToLower(), (BCrypt.Net.BCrypt.HashPassword(user.Value), new User(user.Key)));
            }
        }

        public Task<bool> AddUser(string username, string password)
        {
            if(_users.ContainsKey(username.ToLower()))
            {
                return Task.FromResult(false);
            }
            _users.Add(username.ToLower(), (BCrypt.Net.BCrypt.HashPassword(password), new User(username)));
            return Task.FromResult(true);
        }

        public Task<bool> ValidateCredentials(string username, string password, out User user)
        {
            user = null;

            var key = username.ToLower();
            if(_users.ContainsKey(key))
            {
                var hash = _users[key].passwordHash;
                if(BCrypt.Net.BCrypt.Verify(password,hash))
                {
                    user = _users[key].user;
                    return Task.FromResult(true);
                }
            }

            return Task.FromResult(false);
        }
    }
}
