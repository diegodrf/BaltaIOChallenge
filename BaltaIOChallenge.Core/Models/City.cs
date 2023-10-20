using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BaltaIOChallenge.Core.DTOs;

namespace BaltaIOChallenge.Core.Models;

public class City
{
    [JsonIgnore]
    [Key]
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string State { get; set; } = null!;
    public string Name { get; set; } = null!;
    
    public City() { }

    public City(string code, string state, string name)
    {
        Code = code;
        State = state;
        Name = name;
    }
}
    