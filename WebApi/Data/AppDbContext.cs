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
            new() { Name = "Liters", Type = UnitType.Volume },
            new() { Name = "Pounds", Type = UnitType.Weight },
            new() { Name = "Grams", Type = UnitType.Mass },
            new() { Name = "Milliliters", Type = UnitType.Volume }
        ];
        modelBuilder.Entity<Unit>().HasData(unitsData);
    }
}