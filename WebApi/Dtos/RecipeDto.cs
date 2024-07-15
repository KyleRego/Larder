namespace Larder.Dtos;

public class RecipeDto
{
    public string? RecipeId { get; set; }

    public required string RecipeName { get; set; }

    public required List<RecipeDtoIngredient> Ingredients { get; set; }
}

public class RecipeDtoIngredient
{
    public required string IngredientName { get; set; }

    public string? IngredientId { get; set; }

    public double Amount { get; set; }

    public string? UnitId { get; set; }
}