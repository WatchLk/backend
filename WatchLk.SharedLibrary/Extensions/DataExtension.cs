using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WatchLk.SharedLibrary.Services
{
    public static class DataExtension
    {
        public static async Task ApplyPendingMigrationsAsync<TContext>(this IServiceProvider serviceProvider) where TContext:DbContext
        {
            var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();
            await dbContext.Database.MigrateAsync();
        }
    }
}
