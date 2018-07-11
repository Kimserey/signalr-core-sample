using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace SignalRCoreExample.Identity
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddInMemoryClients(new[] {
                    new Client {
                        ClientName = "signalr-app",
                        ClientSecrets = { new Secret("secret".Sha256()) },
                        AllowedScopes = { "signalr" },
                        AllowedGrantTypes = GrantTypes.ClientCredentials
                    }
                })
                .AddInMemoryApiResources(new[] {
                    new ApiResource("signalr", "SignalR Test API")
                })
                .AddInMemoryIdentityResources(new List<IdentityResource> {
                    new IdentityResources.OpenId(),
                    new IdentityResources.Profile(),
                    new IdentityResources.Email()
                })
                .AddInMemoryPersistedGrants()
                .AddTestUsers(new List<TestUser>{
                    new TestUser { SubjectId = "Alice", Password = "1234", Claims = new[] { new Claim("role", "admin") } }
                });
        }

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
