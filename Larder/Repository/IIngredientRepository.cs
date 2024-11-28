using Larder.Models;

namespace Larder.Repository;

public interface IIngredientRepository
                : IRepositoryBase<Item, IngredientSortOptions>
{
    public Task<Item> FindOrCreateBy(string userId, string name);
}
