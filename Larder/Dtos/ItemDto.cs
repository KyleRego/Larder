using Larder.Models;
using Larder.Models.ItemComponent;

namespace Larder.Dtos;

public class ItemDto
{
    public string? Id { get; set; }
    public required string Name { get; set; }
    public required int Amount { get; set; }
    public string? Description { get; set; }
    public FoodDto? Food { get; set; }
    public IngredientDto? Ingredient { get; set; }
    public QuantityDto? Quantity { get; set; }

    public static ItemDto FromEntity(Item item)
    {
        ItemDto itemDto = new()
        {
            Id = item.Id,
            Name = item.Name,
            Amount = item.Amount,
            Description = item.Description
        };

        Food? food = item.Food;
        if (food != null)
        {
            FoodDto foodDto = new()
            {
                 Id = food.Id,
                 Servings = food.Servings,
                 ServingSize = QuantityDto.FromEntity(food.ServingSize),
                 Calories = food.Calories,
            };
            itemDto.Food = foodDto;
        }

        return itemDto;
    }
}