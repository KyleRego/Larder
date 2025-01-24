using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Larder.Models.ItemComponent;

namespace Larder.Models;

/// <summary>
/// Join model between recipes and ingredients.
/// Associates an ingredient to a recipe
/// also with the amount of the ingredient needed
/// in the recipe.
/// </summary>
public class RecipeIngredient(  string userId,
                                string recipeId,
                                string ingredientId) 
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

    public required Quantity Quantity { get; set; } = new() { Amount = 1 };

    public Quantity QuantityAvailable()
    {
        if (Ingredient == null)
            throw new ApplicationException("Recipe ingredient has null ingredient");
        if (Ingredient.Item == null)
            throw new ApplicationException("Recipe ingredient ingredient has null item");
        if (Ingredient.Item.QuantityComp == null)
            throw new ApplicationException($"Ingredient item {Ingredient.Item} does not have a quantity");
        
        return Ingredient.Item.QuantityComp.Quantity;
    }

    public void SetItemQuantity(Quantity quantity)
    {
        if (Ingredient.Item.QuantityComp == null)
            throw new ApplicationException("Ingredient item does not have a quantity");

        Ingredient.Item.QuantityComp.Quantity = quantity;
    }

    public string Name()
    {
        return Ingredient.Item.Name;
    }
}
