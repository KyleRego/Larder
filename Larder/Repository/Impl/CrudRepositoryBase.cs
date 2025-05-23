using Microsoft.EntityFrameworkCore;

using Larder.Repository.Impl;
using Larder.Repository.Interface;
using Larder.Models;

namespace Larder.Repository.Impl;

public abstract class CrudRepositoryBase<T>(AppDbContext dbContext)
                                : ICrudRepositoryBase<T>
                                            where T : UserOwnedEntity
{
    protected readonly AppDbContext _dbContext = dbContext;
    protected readonly DbSet<T> _dbSet = dbContext.Set<T>();
    public async Task<T> Get(string userId, string id)
    {
        return await GetOrNull(userId, id)
            ?? throw new ApplicationException($"{typeof(T).Name} with ID {id} not found");
    }

    public abstract Task<T?> GetOrNull(string userId, string id);

    public async Task<T> Insert(T newEntity)
    {
        ArgumentNullException.ThrowIfNull(newEntity);

        _dbContext.Add(newEntity);

        await _dbContext.SaveChangesAsync();

        return newEntity;
    }

    public async Task<List<T>> InsertAll(List<T> newEntities)
    {
        ArgumentNullException.ThrowIfNull(newEntities);

        await _dbSet.AddRangeAsync(newEntities);
        await _dbContext.SaveChangesAsync();

        return [.. newEntities];
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

    public void Detach(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Detached;
    }
}
