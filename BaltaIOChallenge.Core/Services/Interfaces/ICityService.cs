using BaltaIOChallenge.Core.Models;

namespace BaltaIOChallenge.Core.Services.Interfaces;

public interface ICityService
{
    Task<IEnumerable<City>> GetByCityAsync(string city);
    Task<IEnumerable<City>> GetByStateAsync(string state);
    Task<City?> GetByCodeAsync(string code);

    Task<City> AddCityAsync(City newCity);

    Task<City> UpdateCityAsync(City newCity);
    
    Task RemoveCityAsync(string code);
}