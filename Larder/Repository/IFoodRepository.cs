using Larder.Models;

namespace Larder.Repository;

public interface IFoodRepository : IRepositoryBase<Item, FoodSortOptions>
{
    public Task<Item> FindOrCreateBy(string userId, string name);
}