using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WatchLk.ProductProcessing.Infrastructure
{
    public static class DataExtension
    {
        public static async Task MapMigrationsAsync(this IServiceProvider app)
        {
            using var scope = app.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
            await dbContext.Database.MigrateAsync();
        }
    }
}
