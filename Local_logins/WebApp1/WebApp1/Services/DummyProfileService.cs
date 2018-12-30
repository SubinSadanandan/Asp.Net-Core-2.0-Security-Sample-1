using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp1.Services
{
    public class DummyProfileService : IProfileService
    {
        private IDictionary<string, UserProfile> _profiles;

        public DummyProfileService(IDictionary<string,UserProfile> profiles)
        {
            _profiles = profiles;
        }

        public Task<UserProfile> GetUserProfileASync(string userId)
        {
            if(_profiles.ContainsKey(userId))
            {
                return Task.FromResult(_profiles[userId]);
            }

            return Task.FromResult<UserProfile>(null);
        }
    }
}
