using BaltaIOChallenge.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BaltaIOChallenge.Api.Infrastructure;

public class AppDbContext: DbContext
{
    public DbSet<Ibge> Ibge { get; set; } = null!;
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}