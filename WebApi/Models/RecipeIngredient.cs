using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Larder.Models;

/// <summary>
/// Join model between recipes and ingredients.
/// Associates an ingredient to a recipe
/// also with the amount of the ingredient needed
/// in the recipe.
/// </summary>
public class RecipeIngredient : UserOwnedEntity
{
    [Required]
    public required string RecipeId { get; set; }

    [ForeignKey(nameof(RecipeId))]
    public Recipe? Recipe { get; set; }

    [Required]
    public required string IngredientId { get; set; }

    [ForeignKey(nameof(IngredientId))]
    public Ingredient Ingredient { get; set; } = null!;

    public required Quantity Quantity { get; set; } = new() { Amount = 1 };
}
