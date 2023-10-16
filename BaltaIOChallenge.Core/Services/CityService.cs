using BaltaIOChallenge.Core.Infrastructure;
using BaltaIOChallenge.Core.Models;
using BaltaIOChallenge.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaltaIOChallenge.Core.Services;

public class CityService : ICityService
{
    private readonly AppDbContext _dbContext;

    public CityService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<City>> GetByCityAsync(string city)
    {
        return await _dbContext.Cities
            .Where(x => x.Name.ToUpper() == city.ToUpper())
            .ToListAsync();
    }

    public async Task<IEnumerable<City>> GetByStateAsync(string state)
    {
        return await _dbContext.Cities
            .Where(x => x.State.ToUpper() == state.ToUpper())
            .ToListAsync();
    }

    public async Task<City?> GetByCodeAsync(string code)
    {
        if(!long.TryParse(code, out _))
        {
            throw new ArgumentException("Code is not a valid number.");
        }
        
        return await _dbContext.Cities
            .FirstOrDefaultAsync(x => x.Code == code);
    }

    public async Task<City> AddCityAsync(City newCity)
    {
        var alreadyExist = await GetByCodeAsync(newCity.Code);
        if (alreadyExist is not null)
        {
            throw new ArgumentException("City data already exists.");
        }

        await _dbContext.Cities.AddAsync(newCity);
        await _dbContext.SaveChangesAsync();
        
        return newCity;
    }

    public async Task<City> UpdateCityAsync(City newCity)
    {
        var city = await GetByCodeAsync(newCity.Code);
        ArgumentNullException.ThrowIfNull(city);

        city.Code = newCity.Code;
        city.Name = newCity.Name;
        city.State = newCity.State;
        
        await _dbContext.SaveChangesAsync();
        
        return city;
    }

    public async Task RemoveCityAsync(string code)
    {
        var city = await GetByCodeAsync(code);
        ArgumentNullException.ThrowIfNull(city);

        _dbContext.Cities.Remove(city);
        
        await _dbContext.SaveChangesAsync();
    }
}