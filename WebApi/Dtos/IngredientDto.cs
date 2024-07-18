namespace Larder.Dtos;

public class IngredientDto
{
    public string? Id { get; set; }

    public required string Name { get; set; }

    public required double Quantity { get; set; }

    public string? Unit { get; set; }

    public List<IngredientRecipeDto> Recipes { get; set; } = [];
}

public class IngredientRecipeDto
{
    public required string Id { get; set; }

    public required string Name { get; set; }
}

public class IngredientQuantityDto
{
    public required string Id { get; set; }

    public required double Quantity { get; set; }
}