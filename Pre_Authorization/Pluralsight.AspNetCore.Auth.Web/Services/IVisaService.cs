using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pluralsight.AspNetCore.Auth.Web.Services
{
    public interface IVisaService
    {
        Task<bool> UserHasVisaForCountryAsync(string username, string country);
    }
}
