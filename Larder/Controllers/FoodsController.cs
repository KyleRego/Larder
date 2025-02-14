using Microsoft.AspNetCore.Mvc;

using Larder.Dtos;
using Larder.Services.Interface;
using Larder.Models.SortOptions;
using System.Globalization;

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
                                EatFood(string id, EatFoodDto dto)
    {
        if (dto.ItemId != id) return BadRequest();

        try
        {
            (ItemDto updatedFood, ItemDto eatenFood) = await _foodService.EatFood(dto);

            return new ApiResponse<ItemDto>(updatedFood, "Food eaten!", ApiResponseType.Success);
        }
        catch(ApplicationException e)
        {
            return UnprocessableEntity(FromError(e));
        }
    }

    [HttpGet("ConsumedFoods")]
    public async Task<ActionResult<List<ItemDto>>> ConsumedFoods(string day)
    {
        if (!DateTime.TryParseExact(day, "yyyy-MM-dd",
                                    CultureInfo.InvariantCulture,
                                    DateTimeStyles.None,
                                    out DateTime parsedDay))
        {
            return BadRequest("Day query parameter was not valid format:");
        }

        return await _foodService.GetConsumedFoods(parsedDay);
    }
}
