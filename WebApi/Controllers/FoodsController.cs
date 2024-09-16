using Microsoft.AspNetCore.Mvc;

using Larder.Repository;
using Larder.Services;
using Larder.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace Larder.Controllers;

[ApiController, Route("api/[controller]")]
public class FoodsController(IFoodService foodService) : ControllerBase
{
    private readonly IFoodService _foodService = foodService;

    [HttpGet("{id}")]
    public async Task<ActionResult<FoodDto>> Show(string id)
    {
        FoodDto? food = await _foodService.GetFood(id);

        return (food == null) ? NotFound() : food;
    }

    [HttpGet, Authorize]
    public async Task<ActionResult<List<FoodDto>>> Index(string? sortOrder, string? search)
    {
        if (sortOrder != null && Enum.TryParse(sortOrder, out FoodSortOptions sortBy))
        {
            return await _foodService.GetFoods(sortBy, search);
        }
        else
        {
            return await _foodService.GetFoods(FoodSortOptions.AnyOrder, search);
        }
    }

    [HttpPost]
    public async Task<ActionResult<FoodDto>> Create(FoodDto food)
    {
        if (food.Id != null) return BadRequest();

        return await _foodService.CreateFood(food);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<FoodDto>> Update(string id, FoodDto food)
    {
        if (food.Id != id) return BadRequest();

        try
        {
            return await _foodService.UpdateFood(food);
        }
        catch(ApplicationException)
        {
            return NotFound();
        }
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<FoodDto>> UpdateQuantity(string id, FoodServingsDto dto)
    {
        if (dto.FoodId != id) return BadRequest();

        try
        {
            return await _foodService.UpdateServings(dto);
        }
        catch(ApplicationException)
        {
            return NotFound();
        }
    }

    [HttpPost("EatFood/{id}")]
    public async Task<ActionResult<FoodDto>> ConsumeServings(string id, FoodServingsDto dto)
    {
        if (dto.FoodId != id) return BadRequest();

        try
        {
            (FoodDto result, ConsumedFoodDto _) = await _foodService.EatFood(dto);

            return result;
        }
        catch(ApplicationException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            await _foodService.DeleteFood(id);
            return Ok();
        }
        catch (ApplicationException)
        {
            return NotFound();
        }
    }
}
