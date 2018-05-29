using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer(options =>
            {
                // http://docs.identityserver.io/en/release/reference/options.html#refoptions
                options.Endpoints = new EndpointsOptions
                {
                    // в Implicit Flow используется для получения токенов
                    EnableAuthorizeEndpoint = true,
                    // для получения статуса сессии
                    EnableCheckSessionEndpoint = true,
                    // для логаута по инициативе пользователя
                    EnableEndSessionEndpoint = true,
                    // для получения claims аутентифицированного пользователя 
                    // http://openid.net/specs/openid-connect-core-1_0.html#UserInfo
                    EnableUserInfoEndpoint = true,
                    // используется OpenId Connect для получения метаданных
                    EnableDiscoveryEndpoint = true,

                    // для получения информации о токенах, мы не используем
                    EnableIntrospectionEndpoint = false,
                    // нам не нужен т.к. в Implicit Flow access_token получают через authorization_endpoint
                    EnableTokenEndpoint = false,
                    // мы не используем refresh и reference tokens 
                    // http://docs.identityserver.io/en/release/topics/reference_tokens.html
                    EnableTokenRevocationEndpoint = false
                };

                // IdentitySever использует cookie для хранения своей сессии
                options.Authentication = new IdentityServer4.Configuration.AuthenticationOptions
                {
                    CookieLifetime = TimeSpan.FromDays(1)
                };

            })
        // тестовый x509-сертификат, IdentityServer использует RS256 для подписи JWT
        .AddDeveloperSigningCredential()
        // что включать в id_token
        .AddInMemoryIdentityResources(Config.IdentityResources)
        // что включать в access_token
        .AddInMemoryApiResources(Config.Apis)
        // настройки клиентских приложений
        .AddInMemoryClients(Config.Clients)
        // тестовые пользователи
        .AddTestUsers(Config.GetUsers());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
        }
    }
}
