using IdentityModel.AspNetCore.OAuth2Introspection;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Example
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();

            services.AddMvc();

            services
                .AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "my-api";
                    options.NameClaimType = "sub";
                    options.TokenRetriever = new Func<HttpRequest, string>(req =>
                    {
                        var fromHeader = TokenRetrieval.FromAuthorizationHeader();
                        var fromQuery = TokenRetrieval.FromQueryString();
                        return fromHeader(req) ?? fromQuery(req);
                    });
                });

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryClients(new[] {
                    new Client {
                        ClientId = "my-app",
                        ClientName = "my-app",
                        ClientSecrets = { new Secret("secret".Sha256()) },
                        AllowedScopes = { "my-api" },
                        AllowedGrantTypes = GrantTypes.ResourceOwnerPassword
                    }
                })
                .AddInMemoryApiResources(new[] {
                    new ApiResource("my-api", "SignalR Test API")
                })
                .AddInMemoryIdentityResources(new List<IdentityResource> {
                    new IdentityResources.OpenId(),
                    new IdentityResources.Profile(),
                    new IdentityResources.Email()
                })
                .AddInMemoryPersistedGrants()
                .AddTestUsers(new List<TestUser>{
                    new TestUser {
                        SubjectId = "alice",
                        Username = "alice",
                        Password = "password",
                        Claims = new[] { new Claim("role", "admin") }
                    }
                });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseSignalR(hubRouteBuilder => {
                hubRouteBuilder.MapHub<ChatHub>("/chathub");
            });

            app.UseIdentityServer();

            app.UseMvc();
        }
    }
}
