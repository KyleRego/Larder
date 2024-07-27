using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Larder.Models;

/// <summary>
/// An ingredient is used in cooking a food
/// </summary>
public class Ingredient : Item
{
    public double Quantity { get; set; }

    [ForeignKey(nameof(UnitId))]
    public Unit? Unit { get; set; }

    public string? UnitId { get; set; }

    public List<RecipeIngredient> RecipeIngredients { get; set; } = [];

    public List<Recipe> Recipes { get; set; } = [];
}