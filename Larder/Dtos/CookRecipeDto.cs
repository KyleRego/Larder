namespace Larder.Dtos;

public class CookRecipeDto
{
    public required string RecipeId { get; set; }
    public required List<CookRecipeIngredientDto> Ingredients { get; set; }
    public required int ServingsProduced { get; set; }
}

public class CookRecipeIngredientDto
{
    public required string IngredientItemId { get; set; }
    public required QuantityDto QuantityCooked { get; set; }
}
