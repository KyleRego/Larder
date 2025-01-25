using Larder.Dtos;
using Larder.Models;
using Larder.Models.ItemComponent;
using Larder.Repository;
using Larder.Services.Interface;

namespace Larder.Services.Impl;

public class ItemService(IServiceProviderWrapper serviceProvider,
                                        IItemRepository itemData)
                            : AppServiceBase(serviceProvider), IItemService
{
    private readonly IItemRepository _itemData = itemData;

    public async Task<ItemDto> CreateItem(ItemDto itemDto)
    {
        string userId = CurrentUserId();

        Item item = new(userId, itemDto.Name, itemDto.Amount, itemDto.Description);

        if (itemDto.QuantityComp != null)
        {
            QuantityComponentDto quantCompDto = itemDto.QuantityComp;

            QuantityComponent quantityComponent = new()
            {
                Item = item,
                Quantity = Quantity.FromDto(quantCompDto.Quantity),
                QuantityPerItem = quantCompDto.QuantityPerItem != null ?
                         Quantity.FromDto(quantCompDto.QuantityPerItem) : null
            };
            item.QuantityComp = quantityComponent;
        }

        item.Food = (itemDto.Food != null) ? Food.FromDto(itemDto.Food, item) : null;

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
        item.Amount = itemDto.Amount;
        item.Description = itemDto.Description;

        item.Food = (itemDto.Food != null) ? Food.FromDto(itemDto.Food, item) : null;
        
        return ItemDto.FromEntity(await _itemData.Update(item));
    }
}
