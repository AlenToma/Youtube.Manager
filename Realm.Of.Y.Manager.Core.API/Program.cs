using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Realm.Of.Y.Manager.Core.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
                //.UseKestrel(options => options.Limits.MaxRequestBodySize = 52428800);
    }
}
