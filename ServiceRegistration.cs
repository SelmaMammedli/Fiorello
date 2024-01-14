using Fiorello.DAL;
using Microsoft.EntityFrameworkCore;

namespace Fiorello
{
    public static class ServiceRegistration
    {
        public static void Register(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllersWithViews();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("Default"));
            });

        } 
    }
}
