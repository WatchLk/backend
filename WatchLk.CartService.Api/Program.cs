using WatchLk.CartService.Api.Endpoints;
using WatchLk.CartService.Application;
using WatchLk.CartService.Infrastructure;
using WatchLk.SharedLibrary.DependencyInjection;
using WatchLk.SharedLibrary.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();

builder.Services.AddSharedServices<AppDbContext>(builder.Configuration);

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("client_only", option =>
    {
        option.RequireRole("client");
    });

var app = builder.Build();

//Apply pendigs migrations
await app.Services.ApplyPendingMigrationsAsync<AppDbContext>();

app.UseAuthentication();

app.UseAuthorization();

app.MapCartEndpoints();

app.Run();
