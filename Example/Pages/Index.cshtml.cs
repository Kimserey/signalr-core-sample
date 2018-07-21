using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Example.Pages
{
    public class IndexModel : PageModel
    {
        public string Token { get; set; }

        //public async Task OnGetAsync()
        //{
        //    var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
        //    var tokenClient = new TokenClient(disco.TokenEndpoint, "my-app", "secret");
        //    var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("alice", "password", "my-api");
        //    Token = tokenResponse.Json.Value<string>("access_token");
        //}
    }
}