using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pluralsight.AspNetCore.Auth.Web.Services
{
    public class DummyVisaService : IVisaService
    {
        private readonly IDictionary<string, string[]> _visaHolders;

        public DummyVisaService(IDictionary<string, string[]> visaHolders)
        {
            _visaHolders = visaHolders;
        }

        public Task<bool> UserHasVisaForCountryAsync(string username, string country)
        {
            if (_visaHolders.ContainsKey(username))
            {
                return Task.FromResult(_visaHolders[username].Contains(country));
            }
            return Task.FromResult(false);
        }
    }
}
