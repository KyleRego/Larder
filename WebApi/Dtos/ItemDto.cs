using Larder.Models;

namespace Larder.Dtos;

public class ItemDto
{
    public string? Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public FoodDto? Food { get; set; }
    public IngredientDto? Ingredient { get; set; }

    public static ItemDto FromEntity(Item item)
    {
        ItemDto itemDto = new()
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description
        };

        if (item.Food != null)
        {
            FoodDto foodDto = FoodDto.FromEntity(item.Food);
        }

        return itemDto;
    }
}