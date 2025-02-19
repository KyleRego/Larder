using Larder.Models;

namespace Larder.Repository.Interface;

public interface IRepositoryBase<T> where T : UserOwnedEntity
{
    public Task<T?> Get(string userId, string id);
    public Task<T> Insert(T newEntity);
    public Task<List<T>> InsertAll(List<T> newEntities);
    public Task<T> Update(T editedEntity);
    public Task Delete(T entity);
}
