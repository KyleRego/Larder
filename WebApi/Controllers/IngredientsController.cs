using Microsoft.AspNetCore.Mvc;

using Larder.Dtos;
using Larder.Services;

namespace Larder.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController(IIngredientService ingredientService)
{
    private readonly IIngredientService _ingredientService = ingredientService;

    [HttpGet]
    public async Task<ActionResult<List<IngredientDto>>> Index()
    {
        return await _ingredientService.GetIngredients();
    }
}