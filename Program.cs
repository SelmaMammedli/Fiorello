using Fiorello;
using Fiorello.DAL;
using Fiorello.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.Register(config);
builder.Services.AddIdentity<AppUser, IdentityRole>(identityOption =>
{
    identityOption.Password.RequiredLength = 5;
    identityOption.Password.RequireNonAlphanumeric = true;
    identityOption.Password.RequireDigit = true;
    identityOption.Password.RequireUppercase = true;
    identityOption.Password.RequireLowercase = true;

    identityOption.User.RequireUniqueEmail = true;

  //identityOption.SignIn.RequireConfirmedEmail = true;
    identityOption.Lockout.MaxFailedAccessAttempts = 3;
    identityOption.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
    identityOption.Lockout.AllowedForNewUsers = true;

}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
         );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
