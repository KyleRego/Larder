using Larder.Models;
using Larder.Models.SortOptions;

namespace Larder.Repository.Interface;

public interface IItemRepository : IRepositoryBase<Item>
{
    public Task<List<Item>> GetAll(string userId,
                ItemSortOptions sortOption=ItemSortOptions.AnyOrder,
                string? search = null);
    
    public Task<Item> FindOrCreate(string userId, string name);
}
