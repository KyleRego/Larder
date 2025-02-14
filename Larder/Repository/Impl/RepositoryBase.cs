using Microsoft.EntityFrameworkCore;

using Larder.Data;
using Larder.Repository.Interface;
using Larder.Models;

namespace Larder.Repository.Impl;

public abstract class RepositoryBase<T, TSortOptions>(AppDbContext dbContext)
                                : IRepositoryBase<T, TSortOptions>
                                            where T : EntityBase
{
    protected readonly AppDbContext _dbContext = dbContext;
    protected readonly DbSet<T> _dbSet = dbContext.Set<T>();
    public abstract Task<T?> Get(string userId, string id);
    public abstract Task<List<T>> GetAll(string userId,
                                            TSortOptions sortBy,
                                            string? search);

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
}
