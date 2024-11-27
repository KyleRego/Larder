using Microsoft.AspNetCore.Mvc;

using Larder.Dtos;
using Larder.Services;
using Larder.Repository;

namespace Larder.Controllers;

public class IngredientsController(IIngredientService ingredientService)
                                        : AppControllerBase
{
    private readonly IIngredientService _ingredientService = ingredientService;

    [HttpGet("{id}")]
    public async Task<ActionResult<IngredientDto>> Show(string id)
    {
        IngredientDto? ingredientDto = await _ingredientService.GetIngredient(id);
    
        return (ingredientDto != null) ? ingredientDto : NotFound();
    }

    [HttpGet]
    public async Task<ActionResult<List<IngredientDto>>> Index(string? sortOrder, string? search)
    {
        if (sortOrder != null && Enum.TryParse(sortOrder, out IngredientSortOptions sortBy))
        {
            return await _ingredientService.GetIngredients(sortBy, search);
        }
        else
        {
            return await _ingredientService.GetIngredients(IngredientSortOptions.AnyOrder, search);
        }
    }
}
