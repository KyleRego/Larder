using Larder.Dtos;

namespace Larder.Services.Interface;

public interface IContainerService
{
    Task PutItemInContainer(string containerItemId, string itemId);
    Task RemoveItemFromContainer(string containerItemId, string itemId);
}
