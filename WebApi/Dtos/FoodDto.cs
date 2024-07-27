using Larder.Models;

namespace Larder.Dtos;

public class FoodDto
{
    public string? Id { get; set; }

    public required string Name { get; set; }

    public string? RecipeId { get; set; }

    public int Servings { get; set; }

    public int Calories { get; set; }
}

public static class FoodDtoAssembler
{
    public static FoodDto Assemble(Food food)
    {
        FoodDto dto = new()
        {
            Id = food.Id,
            Name = food.Name,
            Servings = food.Servings,
            Calories = food.Calories
        };

        if (food.Recipe != null)
        {
            dto.RecipeId = food.Recipe.Id;
        }

        return dto;
    }
}
