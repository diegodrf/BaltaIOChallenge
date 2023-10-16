using System.Linq.Expressions;
using BaltaIOChallenge.Api.Infrastructure;
using BaltaIOChallenge.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BaltaIOChallenge.Api.Endpoints;

public static class Endpoints
{
    public static RouteGroupBuilder MapEndpoints(this RouteGroupBuilder routeGroup)
    {
        routeGroup.MapGet("", async (AppDbContext dbContext) =>
            {
                var result = await dbContext.Ibge.ToListAsync();
                return TypedResults.Ok(result);
            })
            .WithTags("All");

        routeGroup.MapGet("state/{state}", async (AppDbContext dbContext, string state) =>
        {
            Expression<Func<Ibge, bool>> predicate = 
                x => x.State.ToUpper() == state.ToUpper();

            var result = await dbContext.Ibge
                .Where(predicate)
                .ToListAsync();
    
            return TypedResults.Ok(result);
        });

        routeGroup.MapGet("code/{ibgeCode}", async (AppDbContext dbContext, string ibgeCode) =>
        {
            var result = await dbContext.Ibge
                .FirstOrDefaultAsync(x => x.IbgeCode == ibgeCode);
    
            return TypedResults.Ok(result);
        });
        
        return routeGroup;
    }
}