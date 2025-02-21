using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Larder.Models.ItemComponents;

namespace Larder.Models;

/// <summary>
/// Join model between recipes and items.
/// </summary>
public class RecipeIngredient(  string userId,
                                string recipeId,
                                string itemId) 
        : UserOwnedEntity(userId)
{
    [Required]
    public string RecipeId { get; set; } = recipeId;

    [ForeignKey(nameof(RecipeId))]
    public Recipe Recipe { get; set; } = null!;

    [Required]
    public string ItemId { get; set; } = itemId;

    [ForeignKey(nameof(ItemId))]
    public Item Ingredient { get; set; } = null!;

    public required Quantity DefaultQuantity { get; set; }
                                        = Quantity.One();
}
