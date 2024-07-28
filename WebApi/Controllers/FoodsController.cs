using Microsoft.AspNetCore.Mvc;

using Larder.Repository;
using Larder.Services;
using Larder.Dtos;

namespace Larder.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FoodsController(IFoodService foodService) : ControllerBase
{
    private readonly IFoodService _foodService = foodService;

    [HttpGet("{id}")]
    public async Task<ActionResult<FoodDto>> Show(string id)
    {
        FoodDto? food = await _foodService.GetFood(id);

        if (food == null)
        {
            return NotFound();
        }

        return food;
    }

    [HttpGet]
    public async Task<ActionResult<List<FoodDto>>> Index(string? sortOrder, string? name)
    {
        if (sortOrder != null && Enum.TryParse(sortOrder, out FoodSortOptions sortBy))
        {
            return await _foodService.GetFoods(sortBy, name);
        }
        else
        {
            return await _foodService.GetFoods(FoodSortOptions.AnyOrder, name);
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
    public async Task<ActionResult<FoodDto>> UpdateQuantity(string id, QuantityDto quantity)
    {
        if (quantity.Id != id) return BadRequest();

        try
        {
            return await _foodService.UpdateQuantity(quantity);
        }
        catch(ApplicationException)
        {
            return NotFound();
        }

    }
}