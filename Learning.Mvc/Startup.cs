using GoogleReCaptcha.V3;
using GoogleReCaptcha.V3.Interface;
using Learning.Infra.Data.Context;
using Learning.Infra.Ioc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Learning.Mvc
{
    public class Startup
    {
        private Microsoft.Extensions.Configuration.IConfiguration _addressUrl;

        public Startup(IConfiguration configuration, Microsoft.Extensions.Configuration.IConfiguration addressUrl)
        {
            Configuration = configuration;
            _addressUrl = addressUrl;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddHttpClient<ICaptchaValidator, GoogleReCaptchaValidator>();
            #region upload big size

            //allow big Size to 2gig
            services.Configure<FormOptions>(options => { options.MultipartBodyLengthLimit = 21147483648; });

            //--< set uploadsize large files >----

            services.Configure<FormOptions>(options =>
            {

                options.ValueLengthLimit = int.MaxValue;

                options.MultipartBodyLengthLimit = int.MaxValue;

                options.MultipartHeadersLengthLimit = int.MaxValue;

            });

            //--</ set uploadsize large files >----




            #endregion

            services.AddRazorPages();
            #region Check Https Redirect
            //services.Configure<MvcOptions>(options => { options.Filters.Add(new RequireHttpsAttribute()); });
            #endregion

            #region Authentication

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            }).AddCookie(options =>
            {
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(43200);
            });

            #endregion

            #region DataBase Context
            //services.AddDbContext<LearningContext>(option =>
            //{
            //    (option.UseSqlServer(Configuration.GetConnectionString("LearningConnection"));

            //});

            //        services.AddDbContext<LearningContext>
            //(options => options.UseSqlServer(Configuration.GetConnectionString("LearningConnection")), ServiceLifetime.Scoped);

            services.AddDbContext<LearningContext>(options =>
      options.UseNpgsql(Configuration.GetConnectionString("MoneyMagnetStoreConnection")));
            #endregion


            services.AddSingleton<IConfiguration>(new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json")
                .Build());

            RegisterServices(services);

            #region html encoder
            services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.Arabic }));
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Response.Redirect("/Home/Error404");
                }
            });
            app.Use(async (context, next) =>
            {
                var addressOne = context.Request.Path.Value.ToString().ToLower().StartsWith("/allFiles/coursefiles");
                var addressTwo = context.Request.Path.Value.ToString().ToLower().StartsWith("/allFiles/course");
                if (addressOne || addressTwo)
                {
                    var callingUrl = context.Request.Headers["Referer"].ToString();
                    if (callingUrl != "" && (callingUrl.StartsWith("") || callingUrl.StartsWith(_addressUrl["localhost:NameAdress"])))
                    //if (callingUrl != "" && (callingUrl.StartsWith("https://localhost:44349") || callingUrl.StartsWith("https://localhost:44349")))
                    {
                        await next.Invoke();
                    }
                    else
                    {
                        context.Response.Redirect("/Login");
                    }
                }
                else
                {
                    await next.Invoke();
                }

            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthorization();

            #region Check Https Redirect
            //var rewriteOptions = new RewriteOptions().AddRedirectToHttps();
            //app.UseRewriter(rewriteOptions);
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                   name: "areas",
                   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public static void RegisterServices(IServiceCollection services)
        {
            DependencyContainer.RegisterServices(services);
        }
    }
}
