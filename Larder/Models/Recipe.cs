using System.ComponentModel.DataAnnotations;
using Larder.Models.ItemComponents;

namespace Larder.Models;

/// <summary>
/// A recipe has many ingredients; 
/// A recipe is the dependent side in one-to-one with food
/// </summary>
public class Recipe(string userId, string name) : UserOwnedEntity(userId)
{
    [Required]
    public string Name { get; set; } = name;

    public List<RecipeIngredient> RecipeIngredients { get; set; } = [];

    public List<Item> Ingredients { get; set; } = [];

    public List<RecipeStep> Steps { get; set; } = [];
}
