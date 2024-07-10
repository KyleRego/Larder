using Larder.Models;
using Larder.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Larder.Controllers;

[ApiController]
[Route("api/{controller}")]
public class RecipesController(IRecipeRepository repository) : ControllerBase
{
    private readonly IRecipeRepository _repository = repository;

    public async Task<List<Recipe>> Index()
    {
        return await _repository.GetRecipes();
    }
}