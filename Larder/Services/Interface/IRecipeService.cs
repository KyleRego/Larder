using Larder.Dtos;
using Larder.Models;
using Larder.Models.SortOptions;

namespace Larder.Services.Interface;

public interface IRecipeService : ICrudServiceBase<RecipeDto, Recipe>
{
    public Task<List<RecipeDto>> GetRecipes(RecipeSortOptions sortBy,
                                                string? searchName);
    public Task<ItemDto> CookRecipe(CookRecipeDto cookRecipeDto);
}
