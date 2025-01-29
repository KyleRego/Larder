using Microsoft.AspNetCore.Mvc;

using Larder.Repository;
using Larder.Dtos;
using Larder.Services.Interface;

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
    public async Task<ActionResult<ApiResponse<object>>>
                                ConsumeServings(string id, FoodServingsDto dto)
    {
        if (dto.FoodId != id) return BadRequest();

        try
        {
            await _foodService.EatFood(dto);

            return new ApiResponse<object>("Food eaten!", ApiResponseType.Success);
        }
        catch(ApplicationException)
        {
            return UnprocessableEntity();
        }
    }
}
