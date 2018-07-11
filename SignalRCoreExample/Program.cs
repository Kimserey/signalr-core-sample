using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace SignalRCoreExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel(opts =>
                {
                    var configuration = opts.ApplicationServices.GetService<IConfiguration>();
                    opts.Listen(IPAddress.Loopback, 5100);
                    opts.Listen(IPAddress.Loopback, 5101, listenOptions =>
                    {
                        listenOptions.UseHttps(configuration["certificate:path"], configuration["certificate:password"]);
                    });
                })
                .UseStartup<Startup>();
    }
}
