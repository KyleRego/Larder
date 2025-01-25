using Larder.Models.ItemComponents;

namespace Larder.Models;

public class Item(string userId, string name, int amount,
                                     string? description = null)
                                                : UserOwnedEntity(userId)
{

    
    public Item(string userId, string name, int amount)
                        : this(userId, name, amount, null) { }

    public string Name { get; set; } = name;

    public string? Description { get; set; } = description;

    public int Amount { get; set; } = amount;

    public Food? Food { get; set; }
    public Ingredient? Ingredient { get; set; }
    public QuantityComponent? QuantityComp { get; set; }

    // TODO: Allow uploading an image for the item

    public Quantity Quantity()
    {
        if (QuantityComp == null)
        {
            return new() { Amount = Amount };
        }
        else
        {
            return QuantityComp.Quantity;
        }
    }
}
