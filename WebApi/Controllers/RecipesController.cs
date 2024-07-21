using Larder.Dtos;
using Larder.Repository;
using Larder.Services;
using Microsoft.AspNetCore.Mvc;

namespace Larder.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController(IRecipeService recipeService) : ControllerBase
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
    public async Task<List<RecipeDto>> Index(string? sortOrder)
    {
        if (sortOrder != null && Enum.TryParse(sortOrder, out RecipeSortOptions sortBy))
        {
            return await _recipeService.GetRecipes(sortBy);
        }
        else
        {
            return await _recipeService.GetRecipes(RecipeSortOptions.AnyOrder);
        }
    }

    [HttpPost]
    public async Task<ActionResult<RecipeDto>> Create(RecipeDto recipeDto)
    {
        return await _recipeService.CreateRecipe(recipeDto);
    }

    [HttpPut("{recipeId}")]
    public async Task<ActionResult<RecipeDto>> Update([FromBody]RecipeDto recipeDto, string recipeId)
    {
        if (recipeDto.RecipeId != recipeId) return BadRequest();

        RecipeDto? result = await _recipeService.UpdateRecipe(recipeDto);

        if (result == null) { return UnprocessableEntity(); }

        return result;
    }
}