using Microsoft.AspNetCore.Mvc;

using Larder.Dtos;
using Larder.Repository;
using Larder.Services.Interface;
using Larder.Models.SortOptions;

namespace Larder.Controllers;

public class RecipesController(IRecipeService recipeService)
                                        : AppControllerBase
{
    private readonly IRecipeService _recipeService = recipeService;

    [HttpGet("{id}")]
    public async Task<ActionResult<RecipeDto>> Show(string id)
    {
        RecipeDto? recipe = await _recipeService.GetRecipe(id);

        if (recipe == null) return NotFound();

        return recipe;
    }

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

    [HttpPost]
    public async Task<ActionResult<RecipeDto>> Create(RecipeDto recipe)
    {
        return await _recipeService.CreateRecipe(recipe);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        await _recipeService.DeleteRecipe(id);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<RecipeDto>>
                            Update([FromBody]RecipeDto recipe, string id)
    {
        if (recipe.Id != id) return BadRequest();

        try
        {
            return await _recipeService.UpdateRecipe(recipe);
        }
        catch (ApplicationException)
        {
            return NotFound();
        }
    }
}