using Larder.Models;

namespace Larder.Services.Interface;

public interface ICrudServiceBase<TDto, TEntity>
    where TDto : class where TEntity : UserOwnedEntity
{
    public Task<TDto?> Get(string id);
    public Task<TDto> Add(TDto dto);
    public Task<TDto> Update(TDto dto);
    public Task Delete(string id);
}
