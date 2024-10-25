namespace Larder.Models;

public class Item : UserOwnedEntity
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public int Amount { get; set; }

    public Food? Food { get; set; }
    public Ingredient? Ingredient { get; set; }

    // TODO: Allow uploading an image for the item

}
