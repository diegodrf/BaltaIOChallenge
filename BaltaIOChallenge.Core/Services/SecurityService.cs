using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BaltaIOChallenge.Core.DTOs;
using BaltaIOChallenge.Core.Models;
using BaltaIOChallenge.Core.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BaltaIOChallenge.Core.Services;

public class SecurityService : ISecurityService
{
    private readonly string _jwtSecretKey;
    
    public SecurityService(IConfiguration configuration)
    {
        var jwtSecretKey = configuration.GetValue<string>("JwtSecretKey");
        ArgumentException.ThrowIfNullOrEmpty(jwtSecretKey);
        _jwtSecretKey = jwtSecretKey;
    }
    
    public AccessToken GenerateToken(User user)
    {
        var key = Encoding.UTF8.GetBytes(_jwtSecretKey);
        
        var limitDate = DateTime.UtcNow.AddMinutes(20);
        
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature);

        var claimsIdentity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, user.Email)
        });
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Expires = limitDate,
            SigningCredentials = signingCredentials
        };
     
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new AccessToken(tokenHandler.WriteToken(token), limitDate);
    }
}