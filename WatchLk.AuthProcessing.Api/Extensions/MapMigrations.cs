using Microsoft.EntityFrameworkCore;
using WatchLk.AuthProcessing.Infrastructure;

namespace WatchLk.AuthProcessing.Api.Extensions
{
    public static class MapMigrations
    {
        public static async Task MigratePendings(this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await dbContext.Database.MigrateAsync();
        }
    }
}
