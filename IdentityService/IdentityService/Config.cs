using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Test;

namespace IdentityService
{
    public class Config
    {
        public static IEnumerable<Client> Clients => new List<Client>
        {
            new Client
            {
                ClientId = "client",

                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ClientCredentials,

                // secret for authentication
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                // scopes that client has access to
                AllowedScopes = {"api1"}
            },
            new Client
            {
                // обязательный параметр, при помощи client_id сервер различает клиентские приложения 
                ClientId = "js",
                ClientName = "JavaScript Client",
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowAccessTokensViaBrowser = true,
                // от этой настройки зависит размер токена, 
                // при false можно получить недостающую информацию через UserInfo endpoint
                AlwaysIncludeUserClaimsInIdToken = true,
                // белый список адресов на который клиентское приложение может попросить
                // перенаправить User Agent, важно для безопасности
                RedirectUris =
                {
                    // адрес перенаправления после логина
                    "http://localhost:5003/callback.html",
                    // адрес перенаправления при автоматическом обновлении access_token через iframe
                    "http://localhost:5003/callback-silent.html"
                },
                PostLogoutRedirectUris = {"http://localhost:5003/index.html"},
                // адрес клиентского приложения, просим сервер возвращать нужные CORS-заголовки
                AllowedCorsOrigins = {"http://localhost:5003"},
                // список scopes, разрешённых именно для данного клиентского приложения
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "api1"
                },

                AccessTokenLifetime = 300, // секунд, это значение по умолчанию
                IdentityTokenLifetime = 3600, // секунд, это значение по умолчанию

                // разрешено ли получение refresh-токенов через указание scope offline_access
                AllowOfflineAccess = false,
            }
        };

        public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

        public static IEnumerable<ApiResource> Apis => new List<ApiResource>
        {
            new ApiResource("api1", "API 1",
                new[] {"name", "role"})
        };
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim("name", "Alice"),
                        new Claim("website", "https://alice.com"),
                        new Claim("role", "user"),
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim("name", "Bob"),
                        new Claim("website", "https://bob.com"),
                        new Claim("role", "admin"),
                    }
                }
            };
        }
    }
}