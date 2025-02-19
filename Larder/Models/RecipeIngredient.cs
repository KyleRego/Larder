using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Larder.Models.ItemComponents;

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
    public Recipe Recipe { get; set; } = null!;

    [Required]
    public string IngredientId { get; set; } = ingredientId;

    [ForeignKey(nameof(IngredientId))]
    public Ingredient Ingredient { get; set; } = null!;

    public required Quantity DefaultQuantity { get; set; }
                                        = Quantity.One();

    public Quantity QuantityAvailable()
    {
        if (Ingredient == null)
            throw new ApplicationException(
                "Recipe ingredient does not have an associated ingredient");
        if (Ingredient.Item == null)
            throw new ApplicationException(
                "Recipe ingredient does not have an associated item");

        return Ingredient.Item.Quantity;
    }

    public void SetItemQuantity(Quantity quantity)
    {
        Ingredient.Item.Quantity = quantity;
    }

    public string Name()
    {
        return Ingredient.Item.Name;
    }
}
