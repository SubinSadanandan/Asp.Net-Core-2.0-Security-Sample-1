using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Test.AspNetCore.IdentityServer
{
    public static class IdentityServerConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("DemoApi"),
                new ApiResource("WebApi")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "AuthWeb",
                    ClientName = "AuthWeb Demo Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = { "https://localhost:44379/signin-oidc" },
                    PostLogoutRedirectUris = new List<string> { "https://localhost:44379/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                },
                new Client
                {
                    ClientId = "WebApp",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new[] { new Secret("My Secret".Sha256()) },
                    AllowedScopes = new List<string> {"DemoApi"}
                },
                new Client
                {
                    ClientId = "Spa",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        "DemoApi"
                    },
                    RedirectUris = { "https://localhost:44379/SignInCallback.html" },
                    PostLogoutRedirectUris = { "https://localhost:44379/SignOutCallback.html" },
                    AllowedCorsOrigins = { "https://localhost:44379" },
                    RequireConsent = false
                },
                new Client
                {
                    ClientId = "WebApp1",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    ClientSecrets = new [] {new Secret("MySecret".Sha256())},
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "DemoApi"
                    },
                    RedirectUris = { "https://localhost:44379/signin-oidc" },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "https://localhost:44379/signout-callback-oidc"
                    },
                    AllowOfflineAccess = true,
                    RequireConsent = true,
                    AccessTokenLifetime = 5
                },
                new Client
                {
                    ClientId = "AuthWeb_Javascript",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = { "https://localhost:44379/SilentSignInCallback.html" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        "WebApi"
                    },
                    AllowedCorsOrigins = { "https://localhost:44379" },
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "Zerokoll",
                    Password = "test",
                    Claims = new[]
                    {
                        new Claim("name","Zerokoll1")
                    }
                }
            };
        }
    }
}
