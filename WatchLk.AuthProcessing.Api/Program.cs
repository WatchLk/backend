using WatchLk.AuthProcessing.Api.Extensions;
using WatchLk.AuthProcessing.Infrastructure;
using WatchLk.SharedLibrary.DependencyInjection;
using WatchLk.SharedLibrary.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Custom configurations and service registrations
builder.Services.AddInfrastructure(builder.Configuration);

//Add shared services
builder.Services.AddSharedServices<AppDbContext>(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClient", p =>
    {
        p.WithOrigins("http://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .SetPreflightMaxAge(TimeSpan.FromMinutes(10));
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowClient");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await app.Services.ApplyPendingMigrationsAsync<AppDbContext>();

app.Run();
