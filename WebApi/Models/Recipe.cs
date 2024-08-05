using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Larder.Models;

/// <summary>
/// A recipe has many ingredients; 
/// A recipe is the dependent side in one-to-one with food
/// </summary>
public class Recipe : EntityBase
{
    [Required(AllowEmptyStrings = false)]
    public required string Name { get; set; }

    public required string FoodId { get; set; }

    [ForeignKey(nameof(FoodId))]
    public Food Food { get; set; } = null!;

    public int ServingsProduced { get; set; }

    public List<RecipeIngredient> RecipeIngredients { get; set; } = [];

    public List<Ingredient> Ingredients { get; set; } = [];

    public List<RecipeStep> Steps { get; set; } = [];
}