namespace Larder.Repository;

public interface IRepositoryBase<T, TSortOptions>
{
    public Task<T?> Get(string userId, string id);

    public Task<List<T>> GetAll(string userId, TSortOptions sortBy,
                                                    string? search);

    public Task<T> Insert(T newEntity);

    public Task<T> Update(T editedEntity);

    public Task Delete(T entity);
}
