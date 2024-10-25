using System.ComponentModel.DataAnnotations;

namespace Larder.Models;

/// <summary>
/// A recipe has many ingredients; 
/// A recipe is the dependent side in one-to-one with food
/// </summary>
public class Recipe : UserOwnedEntity
{
    [Required(AllowEmptyStrings = false)]
    public required string Name { get; set; }

    public int ServingsProduced { get; set; }

    public List<RecipeIngredient> RecipeIngredients { get; set; } = [];

    public List<Ingredient> Ingredients { get; set; } = [];

    public List<RecipeStep> Steps { get; set; } = [];
}
