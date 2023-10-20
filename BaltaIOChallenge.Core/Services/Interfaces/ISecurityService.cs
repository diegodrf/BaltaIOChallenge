using BaltaIOChallenge.Core.DTOs;
using BaltaIOChallenge.Core.Models;

namespace BaltaIOChallenge.Core.Services.Interfaces;

public interface ISecurityService
{
    AccessToken GenerateToken(User user);
}