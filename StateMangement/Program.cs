using Microsoft.EntityFrameworkCore;
using StateMangement.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContextPool<CLSDbContext>(op => op.UseSqlServer("data source=.;database=logInDb;integrated security=true;"));


builder.Services.Configure<CookiePolicyOptions>(option =>
{
    option.CheckConsentNeeded = context => true;
    option.MinimumSameSitePolicy = SameSiteMode.None;
    
});


builder.Services.AddSession();///=>regester session service

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy();

app.UseSession();///==> apply session request pipline


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
