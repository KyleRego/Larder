using Larder.Models.ItemComponents;

namespace Larder.Dtos;

public class QuantityComponentDto
{
    public required QuantityDto Quantity { get; set; }
    public QuantityDto? QuantityPerItem { get; set; }

    public static QuantityComponentDto FromEntity(QuantityComponent quantityComponent)
    {
        QuantityDto? quantityPerItem = quantityComponent.QuantityPerItem != null ?
            QuantityDto.FromEntity(quantityComponent.QuantityPerItem) : null;

        return new()
        {
            Quantity = QuantityDto.FromEntity(quantityComponent.Quantity),
            QuantityPerItem = quantityPerItem
        };
    }
}
