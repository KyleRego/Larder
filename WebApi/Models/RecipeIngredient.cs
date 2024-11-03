using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Larder.Models;

/// <summary>
/// Join model between recipes and ingredients.
/// Associates an ingredient to a recipe
/// also with the amount of the ingredient needed
/// in the recipe.
/// </summary>
public class RecipeIngredient(  string userId,
                                string recipeId,
                                string ingredientId,
                                Quantity? quantity) 
                                            : UserOwnedEntity(userId)
{
    [Required]
    public string RecipeId { get; set; } = recipeId;

    [ForeignKey(nameof(RecipeId))]
    public Recipe? Recipe { get; set; }

    [Required]
    public string IngredientId { get; set; } = ingredientId;

    [ForeignKey(nameof(IngredientId))]
    public Ingredient Ingredient { get; set; } = null!;

    public Quantity Quantity { get; set; } = quantity ?? new() { Amount = 1 };
}
