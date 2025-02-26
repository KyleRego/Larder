using Larder.Models;
using Larder.Models.SortOptions;

namespace Larder.Repository.Interface;

public interface IRecipeRepository : ICrudRepositoryBase<Recipe>
{
    public Task<List<Recipe>> GetAll(string userId,
                RecipeSortOptions sortOption=RecipeSortOptions.AnyOrder,
                string? search = null);
}
