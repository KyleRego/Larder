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
    public QuantityComponentDto? QuantityComp { get; set; }

    public static ItemDto FromEntity(Item item)
    {
        ItemDto itemDto = new()
        {
            Id = item.Id,
            Name = item.Name,
            Amount = item.Amount,
            Description = item.Description,
            Food = (item.Food != null)
                        ? FoodDto.FromEntity(item.Food) : null,

            Ingredient = (item.Ingredient != null)
                        ? IngredientDto.FromEntity(item.Ingredient) : null,

            QuantityComp = (item.QuantityComp != null)
                ? QuantityComponentDto.FromEntity(item.QuantityComp) : null
        };

        return itemDto;
    }
}
