using BaltaIOChallenge.Core.Infrastructure;
using BaltaIOChallenge.Core.Services;
using BaltaIOChallenge.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets<Program>();

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("BaltaIOChallenge");
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<ICityService, CityService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    var appDbContext = app.Services
        .CreateScope()
        .ServiceProvider
        .GetRequiredService<AppDbContext>();
    SeedService.Run(appDbContext);
}

app.UseHttpsRedirection();

app.MapGet("/", async ([FromServices] ICityService cityService) =>
{
    var cities = await cityService.GetByStateAsync("RJ");
    return TypedResults.Ok(cities);
});

app.Run();