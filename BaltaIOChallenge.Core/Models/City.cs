namespace BaltaIOChallenge.Core.Models;

public class City
{
    public string Code { get; set; } = null!;
    public string State { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int Id { get; set; }
    
    public City() { }

    public City(string code, string state, string name)
    {
        Code = code;
        State = state;
        Name = name;
    }
}
    