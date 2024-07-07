using Microsoft.EntityFrameworkCore;
using Larder.Models;

namespace Larder.Data;

public class AppDbContext : DbContext
{
    public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

    public DbSet<Unit> Units { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        Unit[] unitsData = [
            new() { Name = "Liters" },
            new() { Name = "Pounds" },
            new() { Name = "Grams" },
            new() { Name = "Milliliters" }
        ];
        modelBuilder.Entity<Unit>().HasData(unitsData);
    }
}