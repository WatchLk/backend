using WatchLk.ProductProcessing.Api.Endpoints;
using WatchLk.ProductProcessing.Application;
using WatchLk.ProductProcessing.Infrastructure;
using WatchLk.SharedLibrary.DependencyInjection;
using WatchLk.SharedLibrary.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSharedServices<ProductDbContext>(builder.Configuration);

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddAuthorizationBuilder()
        .AddPolicy("admin_only", policy =>
            policy
            .RequireRole("admin")
        );

var app = builder.Build();

await app.Services.ApplyPendingMigrationsAsync<ProductDbContext>();

app.UseAuthentication();
app.UseAuthorization();
app.MapProductEndpoints();

app.Run();
