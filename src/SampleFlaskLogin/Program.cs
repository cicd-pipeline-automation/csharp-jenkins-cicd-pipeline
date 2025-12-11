using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SampleFlaskLogin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Bootstraps the ASP.NET Core host
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Configures and builds the application host.
        /// </summary>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // Uses Startup.cs for dependency injection, routing, middleware, etc.
                    webBuilder.UseStartup<Startup>();
                });
    }
}
