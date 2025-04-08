using Larder.Models;

namespace Larder.Dtos;

public class ItemDto : EntityDto<Item>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public NutritionDto? Nutrition { get; set; }
    public QuantityDto? Quantity { get; set; }
    public bool IsContainer { get; set; }
    public List<ItemDto> ContainedItems { get; set; } = [];

    public static ItemDto FromEntity(Item item)
    {
        ItemDto itemDto = new()
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            Nutrition = (item.Nutrition != null)
                        ? NutritionDto.FromEntity(item.Nutrition) : null,

            Quantity = (item.Quantity != null)
                ? QuantityDto.FromEntity(item.Quantity) : null
        };

        return itemDto;
    }
}
