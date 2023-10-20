using BaltaIOChallenge.Core.DTOs;
using BaltaIOChallenge.Core.Models;

namespace BaltaIOChallenge.Core.Services.Interfaces;

public interface IUserService
{
    Task<User?> GetUserAsync(UserRequest userRequest);
}