using Larder.Models;
using Larder.Models.SortOptions;

namespace Larder.Repository.Interface;

public interface IIngredientRepository : IItemRepository
{
    public Task<List<Item>> GetAll(string userId,
                IngredientSortOptions sortOption=IngredientSortOptions.AnyOrder,
                string? search = null);
}
