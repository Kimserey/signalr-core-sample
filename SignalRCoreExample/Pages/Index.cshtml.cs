using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SignalRCoreExample.Pages
{
    public class IndexModel : PageModel
    {
        public string Token { get; set; }

        public async Task OnGetAsync()
        {
            var disco = await DiscoveryClient.GetAsync("https://localhost:5001");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }
            var tokenClient = new TokenClient(disco.TokenEndpoint, "signalr-app", "secret");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("alice", "password", "signalr");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Token = tokenResponse.Json.Value<string>("access_token");
        }
    }
}
