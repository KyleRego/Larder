using Larder.Models;
using Larder.Repository.Interface;

namespace Larder.Tests.Services.MockRepository;

public abstract class MockCrudRepositoryBase<T> : ICrudRepositoryBase<T>
                        where T : UserOwnedEntity
{
    protected readonly string testUserId = TestUserData.TestUserId();
    protected readonly List<T> _records = [];

    public Task Delete(T entity)
    {
        throw new NotImplementedException();
    }

    public void Detach(T entity)
    {
        return;
    }

    public Task<T> Get(string userId, string id)
    {
        return GetOrNull(userId, id)!;
    }

    public Task<T?> GetOrNull(string userId, string id)
    {
        T? t = _records.FirstOrDefault(thing =>
            thing.Id == id && thing.UserId == userId);

        return Task.FromResult(t);
    }

    public Task<T> Insert(T newEntity)
    {
        _records.Add(newEntity);

        return Task.FromResult(newEntity);
    }

    public Task<List<T>> InsertAll(List<T> newEntities)
    {
        throw new NotImplementedException();
    }

    public async Task<T> Update(T editedEntity)
    {
        T existing = await Get(editedEntity.UserId, editedEntity.Id);
        _records.Remove(existing);
        _records.Add(editedEntity);
        return editedEntity;
    }
}
