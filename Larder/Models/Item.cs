using Larder.Models.ItemComponents;

namespace Larder.Models;

public class Item(string userId, string name, string? description = null)
                                                : UserOwnedEntity(userId)
{
    public Item(string userId, string name) : this(userId, name, null) { }

    public string Name { get; set; } = name;

    public string? Description { get; set; } = description;

    public required Quantity Quantity { get; set; }

    public Nutrition? Food { get; set; }
    public Ingredient? Ingredient { get; set; }

    // TODO: Allow uploading an image for the item
}
