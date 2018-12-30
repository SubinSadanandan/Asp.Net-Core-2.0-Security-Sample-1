using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp1.Services;

namespace WebApp1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvc(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var users = new Dictionary<string, string> { { "Chris","password" } };
            services.AddSingleton<IUserService>(new DummyUserService(users));

            services.AddSingleton<IClaimsTransformation, ProfileClaimsTransformation>();

            var profiles = new Dictionary<string, UserProfile>
            {
                {"Chris",new UserProfile("Chris","klug",new [] { "User" }) }
            };
            services.AddSingleton<IProfileService>(new DummyProfileService(profiles));


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie(options =>
                {
                    options.LoginPath = "/auth/signin";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseRewriter(new Microsoft.AspNetCore.Rewrite.RewriteOptions().AddRedirectToHttps(301,44307));

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
