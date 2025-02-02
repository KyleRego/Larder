using Larder.Models;
using Larder.Models.ItemComponents;

namespace Larder.Dtos;

public class ItemDto
{
    public string? Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public NutritionDto? Nutrition { get; set; }
    public IngredientDto? Ingredient { get; set; }
    public QuantityDto? Quantity { get; set; }

    public static ItemDto FromEntity(Item item)
    {
        ItemDto itemDto = new()
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            Nutrition = (item.Nutrition != null)
                        ? NutritionDto.FromEntity(item.Nutrition) : null,

            Ingredient = (item.Ingredient != null)
                        ? IngredientDto.FromEntity(item.Ingredient) : null,

            Quantity = (item.Quantity != null)
                ? QuantityDto.FromEntity(item.Quantity) : null
        };

        return itemDto;
    }
}
