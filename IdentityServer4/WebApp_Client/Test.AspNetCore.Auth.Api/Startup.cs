using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Test.AspNetCore.Auth.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://localhost:44398/";
                    //options.Audience = "DemoApi";
                    options.Audience = "WebApi";
                    options.TokenValidationParameters.NameClaimType = "name";
                });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            });

            services.AddCors(options =>
            {
                options.AddPolicy("SPA", policy =>
                 {
                     policy.WithOrigins("https://localhost:44379")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                 });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("SPA");

            //app.UseAuthentication();

            app.UseMvc();
        }
    }
}
