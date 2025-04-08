namespace Larder.Services.Interface;

public interface IContainerService
{
    Task<bool> PutItemInContainer(string containerItemId, string itemId);
    Task<bool> RemoveItemFromContainer(string containerItemId, string itemId);

}
