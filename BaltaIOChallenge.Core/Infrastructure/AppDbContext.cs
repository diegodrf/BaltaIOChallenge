using BaltaIOChallenge.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BaltaIOChallenge.Core.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<City> Cities { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<City>().HasIndex(x => x.Code).IsUnique();
        modelBuilder.Entity<City>().HasIndex(x => x.State);
        modelBuilder.Entity<City>().HasIndex(x => x.Name);
        
        modelBuilder.Entity<User>().HasIndex(x => x.Email);
    }
}
