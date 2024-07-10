using System.ComponentModel.DataAnnotations;

namespace Larder.Models;

public class Recipe : EntityBase
{
    [Required(AllowEmptyStrings = false)]
    public required string Name { get; set; }

    public List<RecipeIngredient> RecipeIngredients { get; set; } = [];
}