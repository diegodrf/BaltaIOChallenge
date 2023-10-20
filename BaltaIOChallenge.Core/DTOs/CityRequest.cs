using BaltaIOChallenge.Core.Models;

namespace BaltaIOChallenge.Core.DTOs;

public class CityRequest
{
    public required string Code { get; set; }
    public required string State { get; set; }
    public required string Name { get; set; }

    public static implicit operator City(CityRequest cityRequest)
    {
        return new City(cityRequest.Code, cityRequest.Name, cityRequest.State);
    }
}