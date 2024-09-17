using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Larder.Models;

namespace Larder.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
                                : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Item> Items { get; set; }
    public DbSet<Food> Foods { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
    public DbSet<RecipeStep> RecipeSteps { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<Utensil> Utensils { get; set; }
    public DbSet<ConsumedFood> ConsumedFoods { get; set; }
    public DbSet<UnitConversion> UnitConversions { get; set; }

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
