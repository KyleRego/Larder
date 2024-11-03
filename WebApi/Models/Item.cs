namespace Larder.Models;

public class Item(string userId, string name, string? description = null)
                                                : UserOwnedEntity(userId)
{
    public string Name { get; set; } = name;

    public string? Description { get; set; } = description;

    public int Amount { get; set; }

    public Food? Food { get; set; }
    public Ingredient? Ingredient { get; set; }

    // TODO: Allow uploading an image for the item

}
