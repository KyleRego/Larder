namespace Larder.Repository;

// TODO: Second generic type parameter to take an enum of the sort options
public interface IRepositoryBase<T>
{
    public Task<T?> Get(string id);

    public Task<List<T>> GetAll();

    public Task<T> Insert(T newT);

    public Task<T> Update(T editedT);

    public Task Delete(T t);
}
