using Larder.Dtos;
using Larder.Services;
using Microsoft.AspNetCore.Mvc;

namespace Larder.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController(IRecipeService recipeService) : ControllerBase
{
    private readonly IRecipeService _recipeService = recipeService;

    [HttpGet]
    public async Task<List<RecipeDto>> Index()
    {
        return await _recipeService.GetRecipes();
    }

    [HttpGet("{recipeId}")]
    public async Task<ActionResult<RecipeDto>> Show(string recipeId)
    {
        RecipeDto? recipe = await _recipeService.GetRecipe(recipeId);

        if (recipe == null) return NotFound();

        return recipe;
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