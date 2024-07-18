using Microsoft.EntityFrameworkCore;
using Larder.Models;

namespace Larder.Data;

public class AppDbContext : DbContext
{
    public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

    public DbSet<Ingredient> Ingredients { get; set; } = default!;
    public DbSet<Recipe> Recipes { get; set; } = default!;
    public DbSet<RecipeIngredient> RecipeIngredients { get; set; } = default!;
    public DbSet<Unit> Units { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Unit tablespoons = new() { Name = "Tablespoons", Type = UnitType.Volume };
        Unit cups = new() { Name = "Cups", Type = UnitType.Volume };
        Unit quantity = new() { Name = "Quantity", Type = UnitType.Count };

        Unit[] unitsData = [
            new() { Name = "Liters", Type = UnitType.Volume },
            new() { Name = "Pounds", Type = UnitType.Weight },
            new() { Name = "Grams", Type = UnitType.Mass },
            new() { Name = "Milliliters", Type = UnitType.Volume },
            tablespoons,
            cups,
            quantity
        ];
        modelBuilder.Entity<Unit>().HasData(unitsData);

        Ingredient butter = new() { Name = "Butter" };
        Ingredient water = new() { Name = "Water" };
        Ingredient boxRice = new() { Name = "Rice Roni Chicken Lower Sodium box" };

        Ingredient[] ingredientsData = [
            butter,
            water,
            boxRice
        ];
        modelBuilder.Entity<Ingredient>().HasData(ingredientsData);

        Recipe lowSodiumChickenRice = new()
        {
            Name = "Rice Roni Low Sodium Chicken Rice"
        };
        modelBuilder.Entity<Recipe>().HasData([lowSodiumChickenRice]);
    
        List<RecipeIngredient> lowSodiumChickenRiceIngredients = [
            new() { RecipeId = lowSodiumChickenRice.Id, IngredientId = butter.Id, UnitId = tablespoons.Id, Amount = 1 },
            new() { RecipeId = lowSodiumChickenRice.Id, IngredientId = water.Id, UnitId = cups.Id, Amount = 2.5 },
            new() { RecipeId = lowSodiumChickenRice.Id, IngredientId = boxRice.Id, UnitId = quantity.Id, Amount = 1 }
        ];
        modelBuilder.Entity<RecipeIngredient>().HasData(lowSodiumChickenRiceIngredients);
    }
}