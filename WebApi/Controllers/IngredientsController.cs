using Microsoft.AspNetCore.Mvc;

using Larder.Dtos;
using Larder.Services;
using Larder.Repository;

namespace Larder.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController(IIngredientService ingredientService) : ControllerBase
{
    private readonly IIngredientService _ingredientService = ingredientService;

    [HttpGet("{id}")]
    public async Task<ActionResult<IngredientDto>> Show(string id)
    {
        IngredientDto? ingredientDto = await _ingredientService.GetIngredient(id);
    
        return (ingredientDto != null) ? ingredientDto : NotFound();
    }

    [HttpGet]
    public async Task<ActionResult<List<IngredientDto>>> Index(string? sortOrder, string? name)
    {
        if (sortOrder != null && Enum.TryParse(sortOrder, out IngredientSortOptions sortBy))
        {
            return await _ingredientService.GetIngredients(sortBy, name);
        }
        else
        {
            return await _ingredientService.GetIngredients(IngredientSortOptions.AnyOrder, name);
        }
    }

    [HttpPost]
    public async Task<ActionResult<IngredientDto>> Create(IngredientDto ingredientDto)
    {
        if (ingredientDto.Id != null) return BadRequest();

        ingredientDto = await _ingredientService.CreateIngredient(ingredientDto);

        return ingredientDto;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<IngredientDto>> Update(IngredientDto ingredientDto, string id)
    {
        if (ingredientDto.Id != id) return BadRequest();

        ingredientDto = await _ingredientService.UpdateIngredient(ingredientDto);

        return ingredientDto;
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<IngredientDto>> UpdateQuantity(IngredientQuantityDto ingredient, string id)
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

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        await _ingredientService.DeleteIngredient(id);
        return Ok();
    }
}