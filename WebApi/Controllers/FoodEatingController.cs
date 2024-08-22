using Larder.Dtos;
using Larder.Services;
using Microsoft.AspNetCore.Mvc;

namespace Larder.Controllers;

[ApiController, Route("api/[controller]")]
public class FoodEatingController(IFoodEatingService foodEatingService) : Controller
{
    private readonly IFoodEatingService _foodEatingService = foodEatingService;

    public async Task<IActionResult> Post(FoodEatingLog dto)
    {
        await _foodEatingService.EatFood(dto);

        return Ok();
    }
}
