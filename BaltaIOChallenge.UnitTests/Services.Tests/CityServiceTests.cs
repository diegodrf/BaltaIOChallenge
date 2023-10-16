using Microsoft.EntityFrameworkCore;

namespace BaltaIOChallenge.UnitTests.Services.Tests;

public class CityServiceTests
{
    private readonly ICityService _cityService;
    
    public CityServiceTests()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseInMemoryDatabase("DatabaseForTests");
        var dbContext = new AppDbContext(optionsBuilder.Options);
        _cityService = new CityService(dbContext);
        SeedService.Run(dbContext);
    }
    
    [Fact]
    public async Task GivenAValidCity_ShouldReturnAllCitiesWithThisName()
    {
        var cities = await _cityService.GetByCityAsync("Buritis");
        
        Assert.Equal(2, cities.Count());
    }
    
    [Fact]
    public async Task GivenAnInvalidCity_ShouldReturnAnEmptyArray()
    {
        var cities = await _cityService.GetByCityAsync("Invalid value");
        
        Assert.Empty(cities);
    }
    
    [Fact]
    public async Task GivenAValidState_ShouldReturnAllCitiesWithThisState()
    {
        var cities = await _cityService.GetByStateAsync("RO");
        
        Assert.Equal(67, cities.Count());
    }
    
    [Fact]
    public async Task GivenAnInvalidState_ShouldReturnAnEmptyArray()
    {
        var cities = await _cityService.GetByStateAsync("Invalid value");
        
        Assert.Empty(cities);
    }
    
    [Fact]
    public async Task GivenAValidCode_ShouldReturnACityWithThisCode()
    {
        var city = await _cityService.GetByCodeAsync("1100015");
        
        Assert.NotNull(city);
    }
    
    [Fact]
    public async Task GivenAnInvalidCode_ShouldReturnNull()
    {
        var city = await _cityService.GetByCodeAsync("99999998");
        
        Assert.Null(city);
    }
    
    [Fact]
    public async Task GivenANewCity_WhenTryToAdd_ShouldAddThisCityWithSuccess()
    {
        var city = new City("9999999999", "New city", "New State");
        
        await _cityService.AddCityAsync(city);
        
        Assert.NotEqual(0, city.Id);
    }
    
    [Fact]
    public async Task GivenADuplicatedCity_WhenTryToAdd_ShouldReturnAnException()
    {
        var city = new City("1100106", "New city", "New State");

        await Assert.ThrowsAsync<ArgumentException>(
            async () => await _cityService.AddCityAsync(city));
    }
    
    [Fact]
    public async Task GivenAValidCity_WhenTryToDelete_ShouldRemoveWithSuccess()
    {
        await _cityService.RemoveCityAsync("1100015");

        var city = await _cityService.GetByCodeAsync("1100015");
        
        Assert.Null(city);
    }
    
    [Fact]
    public async Task GivenAnInvalidCity_WhenTryToDelete_ShouldReturnAnException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await _cityService.RemoveCityAsync("999999999"));
    }
    
    [Fact]
    public async Task GivenAValidCity_WhenTryToUpdate_ShouldUpdateWithSuccess()
    {
        var newCity = new City("1100015", "New city", "New State");
        await _cityService.UpdateCityAsync(newCity);
        var city = await _cityService.GetByCodeAsync(newCity.Code);
        
        Assert.Equal("New State", city!.Name);
    }
    
    [Fact]
    public async Task GivenAnInvalidCity_WhenTryToUpdate_ShouldReturnAnException()
    {
        var city = new City("9999999999", "New city", "New State");

        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await _cityService.UpdateCityAsync(city));
    }
    
    [Fact]
    public async Task GivenAnInvalidCode_WhenTryToAdd_ShouldReturnAnException()
    {
        var city = new City("abc", "New city", "New State");

        await Assert.ThrowsAsync<ArgumentException>(
            async () => await _cityService.AddCityAsync(city));
    }
    
    [Fact]
    public async Task GivenAnInvalidCode_WhenTryToRead_ShouldReturnAnException()
    {
        await Assert.ThrowsAsync<ArgumentException>(
            async () => await _cityService.GetByCodeAsync("abc"));
    }
}