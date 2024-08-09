using Larder.Models;

namespace Larder.Dtos;

public class FoodDto : ItemDto, IQuantityDto
{
    public string? RecipeId { get; set; }

    public double Calories { get; set; }

    public double Protein { get; set; }
}

public static class FoodDtoAssembler
{
    public static FoodDto Assemble(Food food)
    {
        return new()
        {
            Id = food.Id,
            RecipeId = food.Recipe?.Id ?? null,
            Name = food.Name,
            Description = food.Description,
            Calories = food.Calories,
            Amount = food.Amount,
            UnitId = food.UnitId,
            UnitName = food.Unit?.Name
        };
    }
}
