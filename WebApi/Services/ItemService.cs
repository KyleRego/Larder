using Larder.Dtos;
using Larder.Models;
using Larder.Repository;

namespace Larder.Services;

public class ItemService(IServiceProviderWrapper serviceProvider,
                                        IItemRepository itemData)
                            : AppServiceBase(serviceProvider), IItemService
{
    private readonly IItemRepository _itemData = itemData;

    public async Task<ItemDto> CreateItem(ItemDto itemDto)
    {
        string userId = CurrentUserId();

        Item item = new()
        {
            UserId = userId,
            Name = itemDto.Name,
            Description = itemDto.Description 
        };

        if (itemDto.Food != null)
        {
            FoodDto foodDto = itemDto.Food;

            Food food = new()
            {
                Item = item,
                Calories = foodDto.Calories,
                Servings = foodDto.Servings
            };
            food.UpdateTotals();
            item.Food = food;
        }

        item = await _itemData.Insert(item);

        return ItemDto.FromEntity(item);
    }

    public async Task DeleteItem(string id)
    {
        Item item = await _itemData.Get(CurrentUserId(), id) ??
            throw new ApplicationException("No item to delete with that id");
    
        await _itemData.Delete(item);
    }

    public async Task<ItemDto?> GetItem(string id)
    {
        Item? item = await _itemData.Get(CurrentUserId(), id);

        if (item == null) return null;

        return ItemDto.FromEntity(item);
    }

    public async Task<List<ItemDto>> GetItems(ItemSortOptions sortBy,
                                                        string? search)
    {
        string userId = CurrentUserId();

        List<Item> items =
                await _itemData.GetAll(userId, sortBy, search);

        return items.Select(ItemDto.FromEntity).ToList();
    }

    public async Task<ItemDto> UpdateItem(ItemDto itemDto)
    {
        ArgumentNullException.ThrowIfNull(itemDto.Id);

        Item item = await _itemData.Get(CurrentUserId(), itemDto.Id) ??
            throw new ApplicationException("Item to update not found");

        item.Name = itemDto.Name;
        item.Description = itemDto.Description;

        FoodDto? foodDto = itemDto.Food;

        if (foodDto == null)
        {
            item.Food = null;
        }
        else
        {
            Food? food = item.Food;

            if (food == null)
            {
                food = new()
                {
                    Item = item,
                    Calories = foodDto.Calories,
                    Servings = foodDto.Servings
                };
                item.Food = food;
            }
            else
            {
                food.Calories = foodDto.Calories;
                food.Servings = foodDto.Servings;
            }     
        }
        
        return ItemDto.FromEntity(await _itemData.Update(item));
    }
}
