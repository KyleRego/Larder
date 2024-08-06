using Larder.Models;

namespace Larder.Dtos;

public class FoodDto : ItemDto
{
    public string? Id { get; set; }

    public string? RecipeId { get; set; }

    public ItemQuantityDto? Quantity { get; set; }

    public double Calories { get; set; }

    public double Protein { get; set; }
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
            Calories = food.Calories
        };

        if (food.Quantity != null)
        {
            dto.Quantity = ItemQuantityDtoAssembler.Assemble(food.Quantity);
        }

        if (food.Recipe != null)
        {
            dto.RecipeId = food.Recipe.Id;
        }

        return dto;
    }
}
