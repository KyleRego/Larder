using Larder.Models;
using Larder.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Larder.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController(IRecipeRepository repository) : ControllerBase
{
    private readonly IRecipeRepository _repository = repository;

    [HttpGet]
    public async Task<List<Recipe>> Index()
    {
        return await _repository.GetRecipes();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Recipe>> Show(string id)
    {
        Recipe? recipe = await _repository.GetRecipe(id);

        if (recipe == null) return NotFound();

        return recipe;
    }
}