using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WatchLk.SharedLibrary.Middleware;

namespace WatchLk.SharedLibrary.DependencyInjection
{
    public static class SharedServiceContainer
    {
        public static IServiceCollection AddSharedServices<TContext>(this IServiceCollection services, IConfiguration configuration) where TContext:DbContext
        {
            // Add generic database context
            services.AddDbContext<TContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DbString"), sqlserveroptions => sqlserveroptions.EnableRetryOnFailure()));

            //Add jwt authentication
            services.AddJwtAuthenticationScheme(configuration);
            return services;
        }
    }
}
