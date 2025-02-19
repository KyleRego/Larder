using Larder.Models;
using Larder.Models.SortOptions;

namespace Larder.Repository.Interface;

public interface IFoodRepository : IItemRepository
{
    public Task<List<Item>> GetAll(string userId,
                FoodSortOptions sortOption=FoodSortOptions.AnyOrder,
                string? search = null);
    public Task<List<Item>> GetConsumedFoods(string userId, DateTime day);
}
