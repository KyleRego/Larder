using Microsoft.EntityFrameworkCore;

using Larder.Data;

namespace Larder.Repository;

public enum SortOptionsBase
{
    AnyOrder
}

public interface IRepositoryBase<T, TSortOptions>
{
    public Task<T?> Get(string id);

    public Task<List<T>> GetAll(TSortOptions sortBy, string? search);

    public Task<T> Insert(T newEntity);

    public Task<T> Update(T editedEntity);

    public Task Delete(T entity);
}

public abstract class RepositoryBase<T, TSortOptions>(AppDbContext dbContext) : IRepositoryBase<T, TSortOptions>
{
    protected readonly AppDbContext _dbContext = dbContext;

    public abstract Task<T?> Get(string id);

    public abstract Task<List<T>> GetAll(TSortOptions sortBy, string? search);

    public async Task<T> Insert(T newEntity)
    {
        ArgumentNullException.ThrowIfNull(newEntity);

        _dbContext.Add(newEntity);

        await _dbContext.SaveChangesAsync();

        return newEntity;
    }

    public async Task<T> Update(T editedEntity)
    {
        ArgumentNullException.ThrowIfNull(editedEntity);

        _dbContext.Entry(editedEntity).State = EntityState.Modified;

        await _dbContext.SaveChangesAsync();

        return editedEntity;
    }

    public async Task Delete(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _dbContext.Entry(entity).State = EntityState.Deleted;

        await _dbContext.SaveChangesAsync();
    }
}
