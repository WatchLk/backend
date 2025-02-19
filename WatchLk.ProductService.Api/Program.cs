using Microsoft.EntityFrameworkCore;
using WatchLk.ProductProcessing.Api.Endpoints;
using WatchLk.ProductProcessing.Application;
using WatchLk.ProductProcessing.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("productDbString"))
);

builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

await app.Services.MapMigrationsAsync();

app.MapProductEndpoints();

app.Run();
