using GoogleReCaptcha.V3.Interface;
using GoogleReCaptcha.V3;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Authentication.Cookies;
using Learning.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Learning.Infra.Ioc;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
ConfigurationManager Configuration = builder.Configuration;

builder.Services.AddHttpClient<ICaptchaValidator, GoogleReCaptchaValidator>();

//allow big Size to 2gig
builder.Services.Configure<FormOptions>(options => { options.MultipartBodyLengthLimit = 21147483648; });

//--< set uploadsize large files >----

builder.Services.Configure<FormOptions>(options =>
{

    options.ValueLengthLimit = int.MaxValue;

    options.MultipartBodyLengthLimit = int.MaxValue;

    options.MultipartHeadersLengthLimit = int.MaxValue;

});
builder.Services.AddRazorPages();
#region Authentication

builder.Services.AddAuthentication(options =>
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


builder.Services.AddDbContext<LearningContext>(options =>
options.UseNpgsql(Configuration.GetConnectionString("MoneyMagnetStoreConnection")));
#endregion


builder.Services.AddSingleton<IConfiguration>(new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile($"appsettings.json")
    .Build());




#region dependency injection
DependencyContainer.RegisterServices(builder.Services);
#endregion
#region html encoder
builder.Services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.Arabic }));
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}




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

    }
    else
    {
        await next.Invoke();
    }

});

app.UseAuthentication();
//app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();
app.UseAuthorization();


app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
     name: "default",
     pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();