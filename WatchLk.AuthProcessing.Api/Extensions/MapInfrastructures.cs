using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WatchLk.AuthProcessing.Application;
using WatchLk.AuthProcessing.Domains.Models;
using WatchLk.AuthProcessing.Infrastructure;

namespace WatchLk.AuthProcessing.Api.Extensions
{
    public static class MapInfrastructures
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("AuthDbString")));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;
            })
                 .AddEntityFrameworkStores<AppDbContext>()
                 .AddDefaultTokenProviders();


            services.AddScoped<IAuthRepository, AuthRepository>();
        }
    }
}
