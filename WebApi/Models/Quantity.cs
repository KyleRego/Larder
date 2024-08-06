using System.ComponentModel.DataAnnotations.Schema;

namespace Larder.Models;

/// <summary>
/// A quantity is a number + unit tuple
/// associated with an item, and also
/// the amount of an ingredient in a recipe.
/// </summary>
public class Quantity : EntityBase
{
    public string? ItemId { get; set; }

    [ForeignKey(nameof(ItemId))]
    public Item? Item { get; set; }

    public string? RecipeIngredientId { get; set; }

    [ForeignKey(nameof(RecipeIngredientId))]
    public RecipeIngredient? RecipeIngredient { get; set; }

    public double Amount { get; set; }

    public string? UnitId { get; set; }

    [ForeignKey(nameof(UnitId))]
    public Unit? Unit { get; set; }
}