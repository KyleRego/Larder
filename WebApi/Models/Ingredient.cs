namespace Larder.Models;

/// <summary>
/// An ingredient is used in cooking a food
/// </summary>
public class Ingredient : Item
{
    public Quantity? Quantity { get; set; }

    public List<RecipeIngredient> RecipeIngredients { get; set; } = [];

    public List<Recipe> Recipes { get; set; } = [];
}
