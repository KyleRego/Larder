using Microsoft.AspNetCore.Mvc;

using Larder.Dtos;
using Larder.Services.Interface;
using Larder.Models.SortOptions;
using Larder.Models;

namespace Larder.Controllers;

public class RecipesController(IRecipeService recipeService)
                                        : CrudControllerBase<RecipeDto, Recipe>(recipeService)
{
    private readonly IRecipeService _recipeService = recipeService;

    [HttpGet]
    public async Task<List<RecipeDto>> Index(string? sortOrder, string? search)
    {
        if (sortOrder != null && Enum.TryParse(sortOrder, out RecipeSortOptions sortBy))
        {
            return await _recipeService.GetRecipes(sortBy, search);
        }
        else
        {
            return await _recipeService.GetRecipes(RecipeSortOptions.AnyOrder, search);
        }
    }
}
