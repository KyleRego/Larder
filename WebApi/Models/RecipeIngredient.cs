using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Larder.Models;

public class RecipeIngredient : EntityBase
{
    [Required]
    public string? RecipeId { get; set; }

    [ForeignKey(nameof(RecipeId))]
    [JsonIgnore]
    public Recipe? Recipe { get; set; }

    [Required]
    public required string IngredientId { get; set; }

    [ForeignKey(nameof(IngredientId))]
    public Ingredient? Ingredient { get; set; }

    [Required]
    public string? UnitId { get; set; }

    [ForeignKey(nameof(UnitId))]
    public Unit? Unit { get; set; }

    [Required]
    public double Amount { get; set; }
}