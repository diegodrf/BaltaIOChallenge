using BaltaIOChallenge.Core.DependencyInjection;
using BaltaIOChallenge.Core.DTOs;
using BaltaIOChallenge.Core.Infrastructure;
using BaltaIOChallenge.Core.Models;
using BaltaIOChallenge.Core.Services;
using BaltaIOChallenge.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets<Program>();

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("BaltaIOChallenge");
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddJwtSecurity(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on text box below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

});

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
    SeedService.CreateAdminUser(appDbContext);
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.MapGet("/state/{state}", async Task<Ok<IEnumerable<City>>> (
        [FromServices] ICityService cityService,
        [FromRoute] string state) =>
{
    var cities = await cityService.GetByStateAsync(state);
    return TypedResults.Ok(cities);
})
    .RequireAuthorization();

app.MapGet("/city/{city}", async Task<Ok<IEnumerable<City>>> (
        [FromServices] ICityService cityService,
        [FromRoute] string city) =>
    {
        var cities = await cityService.GetByCityAsync(city);
        return TypedResults.Ok(cities);
    })
    .RequireAuthorization();

app.MapGet("/code/{code}", async Task<Results<Ok<City>, NotFound>> (
        [FromServices] ICityService cityService,
        [FromRoute] string code) =>
    {
        var city = await cityService.GetByCodeAsync(code); 
        
        if(city is null) return TypedResults.NotFound();
        
        return TypedResults.Ok(city);
    })
    .RequireAuthorization();

app.MapDelete("/code/{code}", async Task<NoContent> (
        [FromServices] ICityService cityService,
        [FromRoute] string code) =>
    {
        await cityService.RemoveCityAsync(code);
        
        return TypedResults.NoContent();
    })
    .RequireAuthorization();

app.MapPut("/", async Task<Ok<City>> (
        [FromServices] ICityService cityService,
        [FromBody] CityRequest newCity) =>
    {
        var city = await cityService.UpdateCityAsync(newCity);
        
        return TypedResults.Ok(city);
    })
    .RequireAuthorization();

app.MapPost("/", async Task<Ok<City>> (
        [FromServices] ICityService cityService,
        [FromBody] CityRequest newCity) =>
    {
        var city = await cityService.AddCityAsync(newCity);
        
        return TypedResults.Ok(city);
    })
    .RequireAuthorization();

app.MapPost("/login", async Task<Results<Ok<AccessToken>, UnauthorizedHttpResult>> (
    [FromServices] ISecurityService securityService,
    [FromServices] IUserService userService,
    [FromBody] UserRequest userRequest) =>
{
    var user = await userService.GetUserAsync(userRequest);
    
    if (user is null) return TypedResults.Unauthorized();
    
    var token = securityService.GenerateToken(user);
    return TypedResults.Ok(token);
})
    .AllowAnonymous();

app.Run();