using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleFlaskLogin.Services;

namespace SampleFlaskLogin
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // ============================================================
        // 🔧 Configure Services (Dependency Injection)
        // ============================================================
        public void ConfigureServices(IServiceCollection services)
        {
            // MVC + Views
            services.AddControllersWithViews();

            // Cookie-based authentication
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/AccessDenied";
                });

            // User service for simple login demo
            services.AddScoped<IUserService, InMemoryUserService>();
        }

        // ============================================================
        // 🚀 Configure Middleware Pipeline
        // ============================================================
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Developer-friendly error pages
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Production error handling
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Enforce HTTPS
            app.UseHttpsRedirection();

            // Static file middleware (wwwroot/)
            app.UseStaticFiles();

            app.UseRouting();

            // Authentication & Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            // Routing pattern for MVC
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
