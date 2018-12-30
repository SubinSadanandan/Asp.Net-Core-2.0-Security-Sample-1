using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace WebApp_Client
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            //    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //})
            //    .AddOpenIdConnect(options =>
            //    {
            //        options.Authority = "https://localhost:44398/";
            //        options.ClientId = "AuthWeb";
            //        options.SaveTokens = true;
            //        options.TokenValidationParameters.NameClaimType = "name";
            //    })
            //    .AddCookie();

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = "oidc";
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = "oidc";
            })
                .AddOpenIdConnect("oidc",options =>
                {
                    options.Authority = "https://localhost:44398/";
                    options.ClientId = "WebApp1";
                    options.ClientSecret = "MySecret";
                    options.ResponseType = OpenIdConnectResponseType.CodeIdToken;
                    options.Scope.Add("DemoApi");
                    options.Scope.Add("offline_access");
                    options.SignedOutRedirectUri = "/";
                    options.SaveTokens = true;
                    options.TokenValidationParameters.NameClaimType = "name";
                    options.GetClaimsFromUserInfoEndpoint = true;
                })
                .AddCookie();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }
}
