using Larder.Dtos;
using Larder.Models;
using Larder.Repository.Interface;
using Larder.Services.Interface;

namespace Larder.Services.Impl;

public class ContainerService(IServiceProviderWrapper serviceProvider,
                    IItemRepository itemData)
                : AppServiceBase(serviceProvider), IContainerService
{
    private readonly IItemRepository _itemData = itemData;

    public async Task<List<ItemDto>> GetContainers()
    {
        List<Item> containers =
            await _itemData.GetAllContainers(CurrentUserId());

        return [.. containers.Select(ItemDto.FromEntity)];
    }

    public async Task<ItemDto> PutItemInContainer(string containerItemId, string itemId)
    {
        string userId = CurrentUserId();

        Item containerItem = await _itemData.Get(userId, containerItemId)
            ?? throw new ApplicationException($"Container item {containerItemId} not found");

        if (containerItem.Container == null)
            throw new ApplicationException($"Item {containerItemId} is not a container");

        Item item = await _itemData.Get(userId, itemId)
            ?? throw new ApplicationException($"Item {itemId} not found");

        containerItem.Container.Items.Add(item);

        Item updatedContainer = await _itemData.Update(containerItem);

        return ItemDto.FromEntity(updatedContainer);
    }

    public async Task<ItemDto> RemoveItemFromContainer(string containerItemId, string itemId)
    {
        string userId = CurrentUserId();

        Item containerItem = await _itemData.Get(userId, containerItemId)
            ?? throw new ApplicationException($"Container item {containerItemId} not found");

        if (containerItem.Container == null)
            throw new ApplicationException($"Item {containerItemId} is not a container");

        Item item = containerItem.Container.Items.FirstOrDefault(item => item.Id == itemId)
            ?? throw new ApplicationException("Item was not in the container");

        containerItem.Container.Items.Remove(item);

        Item updatedContainer = await _itemData.Update(containerItem);

        return ItemDto.FromEntity(updatedContainer);
    }
}