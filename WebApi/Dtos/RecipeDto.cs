namespace Larder.Dtos;

public class RecipeDto
{
    public string? RecipeId { get; set; }

    public required string RecipeName { get; set; }

    public required List<RecipeIngredientDto> Ingredients { get; set; }
}

public class RecipeIngredientDto
{
    public string? RecipeIngredientId { get; set; }

    public required string IngredientName { get; set; }

    public string? IngredientId { get; set; }

    public double Amount { get; set; }

    public string? UnitName { get; set; }

    public string? UnitId { get; set; }
}