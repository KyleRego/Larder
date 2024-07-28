using Larder.Models;

namespace Larder.Dtos;

public class FoodDto : ItemDto
{
    public string? Id { get; set; }

    public string? RecipeId { get; set; }

    public int Quantity { get; set; }

    public int Calories { get; set; }

    public int Protein { get; set; }
}

public static class FoodDtoAssembler
{
    public static FoodDto Assemble(Food food)
    {
        FoodDto dto = new()
        {
            Id = food.Id,
            Name = food.Name,
            Description = food.Description,
            Quantity = food.Quantity,
            Calories = food.Calories
        };

        if (food.Recipe != null)
        {
            dto.RecipeId = food.Recipe.Id;
        }

        return dto;
    }
}
