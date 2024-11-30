using Microsoft.AspNetCore.Mvc;

using Larder.Repository;
using Larder.Services;
using Larder.Dtos;

namespace Larder.Controllers;

public class FoodsController(IFoodService foodService)
                                        : AppControllerBase
{
    private readonly IFoodService _foodService = foodService;

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<ItemDto>>> Show(string id)
    {
        try
        {
            ItemDto? foodItem = await _foodService.GetFood(id);

            if (foodItem == null) return NotFound();

            return new ApiResponse<ItemDto>(foodItem, "", ApiResponseType.Success);
        }
        catch (ApplicationException e)
        {
            return UnprocessableEntity(FromError(e));
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<ItemDto>>> Index(string? sortOrder, string? search)
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

    [HttpPatch("{id}")]
    public async Task<ActionResult<FoodDto>>
                                UpdateQuantity(string id, FoodServingsDto dto)
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
    public async Task<ActionResult<ApiResponse<FoodDto>>>
                                ConsumeServings(string id, FoodServingsDto dto)
    {
        if (dto.FoodId != id) return BadRequest();

        try
        {
            (FoodDto result, ConsumedFoodDto _) = await _foodService.EatFood(dto);

            return new ApiResponse<FoodDto>(result, "Food eaten!",
                                                        ApiResponseType.Success);
        }
        catch(ApplicationException)
        {
            return UnprocessableEntity();
        }
    }
}
