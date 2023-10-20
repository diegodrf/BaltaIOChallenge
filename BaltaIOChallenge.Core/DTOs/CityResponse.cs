using BaltaIOChallenge.Core.Models;

namespace BaltaIOChallenge.Core.DTOs;

public class CityResponse
{
    public required string Code { get; set; }
    public required string State { get; set; }
    public required string Name { get; set; }
}