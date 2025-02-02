using Larder.Models;
using Larder.Models.SortOptions;

namespace Larder.Repository.Interface;

public interface IFoodRepository : IRepositoryBase<Item, FoodSortOptions>
{
    public Task<Item> FindOrCreateBy(string userId, string name);
}