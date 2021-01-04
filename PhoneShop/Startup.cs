using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using PhoneShop.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using PhoneShop.Models.DataModel;
using PhoneShop.Models.AccountModel;
namespace PhoneShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string conString = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<EFDatabaseContext>(option => option.UseSqlServer(conString));
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(conString));

            services.AddTransient<IDataRepository, EFDataRepository>();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                    options.AccessDeniedPath = new PathString("/Account/Login");
                });
            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 44344;
            });
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
           
            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
