using BaltaIOChallenge.Core.DTOs;
using BaltaIOChallenge.Core.Infrastructure;
using BaltaIOChallenge.Core.Models;
using BaltaIOChallenge.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaltaIOChallenge.Core.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _dbContext;

    public UserService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<User?> GetUserAsync(UserRequest userRequest)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(x => 
                x.Email == userRequest.Email 
                && x.Password == userRequest.Password);
    }
}