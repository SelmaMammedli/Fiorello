using Fiorello;
using Fiorello.DAL;
using Fiorello.Hubs;
using Fiorello.Models;
using Fiorello.Services.Implementations;
using Fiorello.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.Register(config);
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IBasketService,BasketService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddSignalR();
builder.Services.AddIdentity<AppUser, IdentityRole>(identityOption =>
{
    identityOption.Password.RequiredLength = 5;
    identityOption.Password.RequireNonAlphanumeric = true;
    identityOption.Password.RequireDigit = true;
    identityOption.Password.RequireUppercase = true;
    identityOption.Password.RequireLowercase = true;

    identityOption.User.RequireUniqueEmail = true;

    identityOption.SignIn.RequireConfirmedEmail = true;
    identityOption.Lockout.MaxFailedAccessAttempts = 3;
    identityOption.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
    identityOption.Lockout.AllowedForNewUsers = true;

}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
});

var app = builder.Build();
app.MapHub<ChatHub>("/chat");//ChatHup yaratdigimiz classin adidir,/chat ise uzantisidi(/adminarea kimi)
app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
         );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
