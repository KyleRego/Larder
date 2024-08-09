using Microsoft.EntityFrameworkCore;
using Larder.Models;

namespace Larder.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Item> Items { get; set; }
    public DbSet<Food> Foods { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
    public DbSet<RecipeStep> RecipeSteps { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<Utensil> Utensils { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Unit tablespoons = new() { Name = "Tablespoons", Type = UnitType.Volume };
        Unit cups = new() { Name = "Cups", Type = UnitType.Volume };

        Unit[] unitsData = [
            new() { Name = "Liters", Type = UnitType.Volume },
            new() { Name = "Pounds", Type = UnitType.Weight },
            new() { Name = "Grams", Type = UnitType.Mass },
            new() { Name = "Milliliters", Type = UnitType.Volume },
            tablespoons,
            cups
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

        Food chickenAndRice = new()
        {
            Name = "Chicken and rice"
        };
        modelBuilder.Entity<Food>().HasData([chickenAndRice]);

        Recipe chickenAndRiceRecipe = new()
        {
            Name = "Rice Roni Low Sodium Chicken Rice",
            FoodId = chickenAndRice.Id
        };
        modelBuilder.Entity<Recipe>().HasData([chickenAndRiceRecipe]);

        List<RecipeIngredient> chickenAndRiceRecipeIngredients = [
            new()
            {
                RecipeId = chickenAndRiceRecipe.Id,
                IngredientId = butter.Id,
                Amount = 1,
                UnitId = tablespoons.Id
            },
            new()
            {
                RecipeId = chickenAndRiceRecipe.Id,
                IngredientId = water.Id,
                Amount = 2.5,
                UnitId = cups.Id
            },
            new()
            {
                RecipeId = chickenAndRiceRecipe.Id,
                IngredientId = boxRice.Id,
                Amount = 1,
                UnitId = null
            }
        ];
        modelBuilder.Entity<RecipeIngredient>().HasData(chickenAndRiceRecipeIngredients);
    }
}
