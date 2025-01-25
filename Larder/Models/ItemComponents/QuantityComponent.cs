namespace Larder.Models.ItemComponents;

public class QuantityComponent : ItemComponent
{
    public required Quantity Quantity { get; set; }

    public Quantity? QuantityPerItem { get; set; }
}
