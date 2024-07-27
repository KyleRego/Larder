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
}