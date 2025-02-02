using Larder.Models;
using Larder.Models.SortOptions;

namespace Larder.Repository.Interface;

public interface IIngredientRepository
                : IRepositoryBase<Item, IngredientSortOptions>
{
    public Task<Item> FindOrCreateBy(string userId, string name);
}
