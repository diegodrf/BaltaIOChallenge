namespace BaltaIOChallenge.Core.DTOs;

public class AccessToken
{
    public string Token { get; init; }

    private readonly DateTime _expireDate;
    public long ExpiresIn => (long)_expireDate.Subtract(DateTime.UtcNow).TotalSeconds;

    public AccessToken(string token, DateTime expireDate)
    {
        Token = token;
        _expireDate = expireDate;
    }
}