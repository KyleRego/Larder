using System.ComponentModel.DataAnnotations;
using Larder.Models.ItemComponent;

namespace Larder.Models;

/// <summary>
/// A recipe has many ingredients; 
/// A recipe is the dependent side in one-to-one with food
/// </summary>
public class Recipe(string userId, string name) : UserOwnedEntity(userId)
{
    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; } = name;

    public int ServingsProduced { get; set; }

    public List<RecipeIngredient> RecipeIngredients { get; set; } = [];

    public List<Ingredient> Ingredients { get; set; } = [];

    public List<RecipeStep> Steps { get; set; } = [];
}
