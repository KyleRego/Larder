using Larder.Dtos;

namespace Larder.Services.Interface;

public interface IContainerService
{
    Task<ItemDto> PutItemInContainer(string containerItemId, string itemId);
    Task<ItemDto> RemoveItemFromContainer(string containerItemId, string itemId);
    Task<List<ItemDto>> GetContainers();
}
