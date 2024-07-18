using Microsoft.AspNetCore.Mvc;

using Larder.Dtos;
using Larder.Services;

namespace Larder.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController(IIngredientService ingredientService) : ControllerBase
{
    private readonly IIngredientService _ingredientService = ingredientService;

    [HttpGet]
    public async Task<ActionResult<List<IngredientDto>>> Index()
    {
        return await _ingredientService.GetIngredients();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IngredientDto>> Show(string id)
    {
        IngredientDto? ingredientDto = await _ingredientService.GetIngredient(id);
    
        return (ingredientDto != null) ? ingredientDto : NotFound();
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<IngredientDto>> Patch(IngredientQuantityDto ingredient, string id)
    {
        if (ingredient.Id != id) return BadRequest();

        IngredientDto? updatedIngredient = await _ingredientService.UpdateQuantity(ingredient);

        if (updatedIngredient != null)
        {
            return updatedIngredient;
        }
        else
        {
            return UnprocessableEntity();
        }
    }
}