using Larder.Dtos;
using Larder.Models.SortOptions;

namespace Larder.Services.Interface;

public interface IItemService
{
    public Task<ItemDto?> GetItem(string id);
    public Task<List<ItemDto>> GetItems(ItemSortOptions sortBy,
                                                string? search);
    public Task<ItemDto> CreateItem(ItemDto itemDto);
    public Task<ItemDto> UpdateItem(ItemDto itemDto);
    public Task DeleteItem(string id);
}
