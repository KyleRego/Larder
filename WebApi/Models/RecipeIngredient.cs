using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Larder.Models;

/// <summary>
/// Join model between recipes and ingredients.
/// Associates an ingredient to a recipe
/// also with the amount of the ingredient needed
/// in the recipe.
/// </summary>
public class RecipeIngredient : EntityBase, IQuantity
{
    [Required]
    public required string RecipeId { get; set; }

    [ForeignKey(nameof(RecipeId))]
    public Recipe? Recipe { get; set; }

    [Required]
    public required string IngredientId { get; set; }

    [ForeignKey(nameof(IngredientId))]
    public Ingredient Ingredient { get; set; } = null!;

    public double Amount { get; set; }

    [DisplayFormat(ConvertEmptyStringToNull = true)]
    public string? UnitId { get; set; }

    [ForeignKey(nameof(UnitId))]
    public Unit? Unit { get; set; }
}