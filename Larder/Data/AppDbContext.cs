using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Larder.Models;
using Larder.Models.ItemComponents;

namespace Larder.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
                                : IdentityDbContext<ApplicationUser>(options)
{
    public required DbSet<Item> Items { get; set; }
    public required DbSet<Nutrition> Foods { get; set; }
    public required DbSet<Recipe> Recipes { get; set; }
    public required DbSet<RecipeIngredient> RecipeIngredients { get; set; }
    public required DbSet<RecipeStep> RecipeSteps { get; set; }
    public required DbSet<Unit> Units { get; set; }
    public required DbSet<Utensil> Utensils { get; set; }
    public required DbSet<UnitConversion> UnitConversions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UnitConversion>()
            .HasOne(uc => uc.Unit)
            .WithMany(u => u.Conversions)
            .HasForeignKey(uc => uc.UnitId);

        modelBuilder.Entity<UnitConversion>()
            .HasOne(uc => uc.TargetUnit)
            .WithMany(u => u.TargetConversions)
            .HasForeignKey(uc => uc.TargetUnitId);
    }
}
