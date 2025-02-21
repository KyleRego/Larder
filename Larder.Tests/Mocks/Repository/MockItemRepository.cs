using Larder.Models;
using Larder.Models.SortOptions;
using Larder.Repository.Interface;

namespace Larder.Tests.Mocks.Repository;

public class MockItemRepository(MockUnitData unitData)
    : MockRepositoryBase, IItemRepository
{
    protected readonly MockUnitData _unitData = unitData;
    protected readonly List<Item> _items = [];

    public Task Delete(Item entity)
    {
        throw new NotImplementedException();
    }

    public Task<Item> FindOrCreate(string userId, string name)
    {
        throw new NotImplementedException();
    }

    public Task<Item?> Get(string userId, string id)
    {
        Item? item = _items.FirstOrDefault(item =>
            item.Id == id && item.UserId == userId);

        return Task.FromResult(item);
    }

    public Task<List<Item>> GetAll(string userId, ItemSortOptions sortOption = ItemSortOptions.AnyOrder, string? search = null)
    {
        throw new NotImplementedException();
    }

    public Task<Item> Insert(Item newEntity)
    {
        _items.Add(newEntity);

        return Task.FromResult(newEntity);
    }

    public Task<List<Item>> InsertAll(List<Item> newEntities)
    {
        throw new NotImplementedException();
    }

    public Task<Item> Update(Item editedEntity)
    {
        return Task.FromResult(editedEntity);
    }
}