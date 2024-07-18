using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Larder.Models;

public class Ingredient : EntityBase
{
    [Required(AllowEmptyStrings = false)]
    public required string Name { get; set; }

    public double Quantity { get; set; }

    [ForeignKey(nameof(UnitId))]
    public Unit? Unit { get; set; }

    public string? UnitId { get; set; }

    public List<RecipeIngredient> RecipeIngredients { get; set; } = [];
}