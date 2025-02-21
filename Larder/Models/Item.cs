using Larder.Dtos;
using Larder.Models.ItemComponents;

namespace Larder.Models;

public class Item(string userId, string name, string? description = null)
                                                : UserOwnedEntity(userId)
{
    public static Item FromDto(ItemDto dto, string userId)
    {
        Quantity quantity = dto.Quantity != null
            ? Quantity.FromDto(dto.Quantity) : Quantity.One();

        Item item = new(userId, dto.Name, dto.Description)
        {
            Quantity = quantity
        };

        item.Nutrition = dto.Nutrition != null
            ? Nutrition.FromDto(dto.Nutrition, item) : null;

        return item;
    }

    public Item(string userId, string name) : this(userId, name, null) { }

    public string Name { get; set; } = name;

    public string? Description { get; set; } = description;

    public required Quantity Quantity { get; set; }

    public Nutrition? Nutrition { get; set; }
    public Ingredient? Ingredient { get; set; }

    public ConsumedTime? ConsumedTime { get; set; }
    // TODO: Allow uploading an image for the item

    public List<RecipeIngredient> RecipeIngredients { get; set; } = [];
    public List<Recipe> Recipes { get; set; } = [];
}
