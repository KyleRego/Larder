using Larder.Dtos;
using Larder.Models.SortOptions;

namespace Larder.Services.Interface;

public interface IRecipeService
{
    public Task<RecipeDto?> GetRecipe(string id);
    public Task<List<RecipeDto>> GetRecipes(RecipeSortOptions sortBy,
                                                string? searchName);
    public Task<RecipeDto> CreateRecipe(RecipeDto recipeDto);
    public Task<RecipeDto> UpdateRecipe(RecipeDto recipeDto);
    public Task CookRecipe(CookRecipeDto cookRecipeDto);
    public Task DeleteRecipe(string id);
}
