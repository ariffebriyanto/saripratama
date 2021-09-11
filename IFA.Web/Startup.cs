using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using ERP.Domain.Objects;
using ERP.Web.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ERP.Web
{
    public class Startup
    {
        private SolutionConfig config;
        private FactoryClass factory;
        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile("appSettings.json").Build();
            contentRoot = env.ContentRootPath;
        }

        public static IConfiguration Configuration { get; private set; }
        public static string startdateserver
        {
            get;
            private set;
        }
        public static string enddateserver
        {
            get;
            private set;
        }
        public static string dateserver
        {
            get;
            private set;
        }

        public static string contentRoot
        {
            get;
            private set;
        }
        public static string tglkirimserver
        {
            get;
            private set;
        }
        public static string seminggu
        {
            get;
            private set;
        }

        public static string setahun
        {
            get;
            private set;
        }
        public static string serverUrl
        {
            get;
            private set;
        }

        public static string namaApp
        {
            get;
            private set;
        }
        public static string namaCompany
        {
            get;
            private set;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            config = new SolutionConfig
            {
                SIF_API_Server = Configuration.GetValue<string>("AppConfig:SIF_API_Server"),
                namaApp = Configuration.GetValue<string>("AppConfig:namaApp"),
                namaCompany = Configuration.GetValue<string>("AppConfig:namaCompany"),
                application = Configuration.GetValue<string>("ApplicationSettings:application")

            };

            factory = new FactoryClass(config);

            services.AddTransient<SolutionConfig>(x => config);

            services.AddTransient<FactoryClass>();

            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
             {
                 option.Cookie.Name = "SIF";
                 option.Cookie.Expiration = TimeSpan.FromHours(12);
                 option.ExpireTimeSpan = TimeSpan.FromHours(12);
                 option.SlidingExpiration = true;
             });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
            });


            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true; options.Providers.Add<BrotliCompressionProvider>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Enable compression
            app.UseResponseCompression();
            app.UseStaticFiles();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            DateTime date = DateTime.Now;
            var start = new DateTime(date.Year, date.Month, 1);
            var end= start.AddMonths(1).AddDays(-1);
            var serverdatenow = date;
            var tglkirim = date.AddDays(2);
            var tgl7 = date.AddDays(7);
            var satutahun = date.AddYears(1);

            setahun= satutahun.ToString("dd MMMM yyyy");
            startdateserver = start.ToString("dd MMMM yyyy");
            enddateserver = end.ToString("dd MMMM yyyy");
            dateserver = serverdatenow.ToString("dd MMMM yyyy");
            tglkirimserver = tglkirim.ToString("dd MMMM yyyy");
            seminggu = tgl7.ToString("dd MMMM yyyy");
            serverUrl = Configuration.GetValue<string>("AppConfig:serverUrl");
            namaApp = Configuration.GetValue<string>("AppConfig:namaApp");
            namaCompany = Configuration.GetValue<string>("AppConfig:namaCompany");
        }


    }
}
