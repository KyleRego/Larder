using Larder.Dtos;
using Larder.Models;
using Larder.Models.SortOptions;

namespace Larder.Services.Interface;

public interface IItemService : ICrudServiceBase<ItemDto, Item>
{
    public Task<List<ItemDto>> GetItems(ItemSortOptions sortBy,
                                                string? search);
    public Task<ItemDto> FindOrCreate(string name);
}
