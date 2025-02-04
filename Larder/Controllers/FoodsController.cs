using Microsoft.AspNetCore.Mvc;

using Larder.Dtos;
using Larder.Services.Interface;
using Larder.Models.SortOptions;

namespace Larder.Controllers;

public class FoodsController(IFoodService foodService) : AppControllerBase
{
    private readonly IFoodService _foodService = foodService;

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

    [HttpPost("EatFood/{id}")]
    public async Task<ActionResult<ApiResponse<ItemDto>>>
                                ConsumeServings(string id, EatFoodDto dto)
    {
        if (dto.ItemId != id) return BadRequest();

        try
        {
            ItemDto result = await _foodService.EatFood(dto);

            return new ApiResponse<ItemDto>(result, "Food eaten!", ApiResponseType.Success);
        }
        catch(ApplicationException)
        {
            return UnprocessableEntity();
        }
    }
}
