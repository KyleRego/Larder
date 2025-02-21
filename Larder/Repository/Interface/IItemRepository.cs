using Larder.Models;
using Larder.Models.SortOptions;

namespace Larder.Repository.Interface;

public interface IItemRepository : IRepositoryBase<Item>
{
    public Task<Item> FindOrCreate(string userId, string name);
    public Task<List<Item>> GetAll(string userId,
                ItemSortOptions sortOption=ItemSortOptions.AnyOrder,
                string? search = null);
    public Task<List<Item>> GetAll(string userId,
                FoodSortOptions sortOption=FoodSortOptions.AnyOrder,
                string? search = null);
    public Task<List<Item>> GetAll(string userId,
                IngredientSortOptions sortOption=IngredientSortOptions.AnyOrder,
                string? search = null);
    public Task<List<Item>> GetConsumedFoods(string userId, DateTime day);
}
