using Larder.Dtos;
using Larder.Models;
using Larder.Repository.Interface;
using Larder.Services.Interface;

namespace Larder.Services.Impl;

public abstract class CrudServiceBase<TDto, TEntity>
                        (IServiceProviderWrapper serviceProvider,
                        ICrudRepositoryBase<TEntity> repository)
        : AppServiceBase(serviceProvider),
            ICrudServiceBase<TDto, TEntity>
    where TDto : EntityDto<TEntity>
    where TEntity : UserOwnedEntity
{
    protected readonly ICrudRepositoryBase<TEntity>
        _repository = repository;

    protected abstract Task<TEntity> MapToEntity(TDto dto);
    protected abstract TDto MapToDto(TEntity entity);

    public async Task<TDto> Add(TDto dto)
    {
        TEntity entity = await MapToEntity(dto);
        TEntity insertedEntity = await _repository.Insert(entity);
        return MapToDto(insertedEntity);
    }

    public async Task Delete(string id)
    {
        TEntity? entity = await _repository.Get(CurrentUserId(), id);

        if (entity != null)
            await _repository.Delete(entity);
    }

    public async Task<TDto?> Get(string id)
    {
        TEntity? entity = await _repository.Get(CurrentUserId(), id);
        return entity == null ? null : MapToDto(entity);
    }

    public async Task<TDto> Update(TDto dto)
    {
        TEntity entity = await MapToEntity(dto);

        TEntity updatedEntity = await _repository.Update(entity);

        return MapToDto(updatedEntity);
    }

    public async Task<List<TDto>> AddAll(List<TDto> dtos)
    {
        List<TEntity> entities = [];

        foreach (TDto dto in dtos)
        {
            TEntity ntt = await MapToEntity(dto);
            entities.Add(ntt);
        }

        List<TEntity> inserted = await _repository.InsertAll(entities);

        return [.. inserted.Select(MapToDto)];
    }
}
