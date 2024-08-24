using Larder.Dtos;
using Larder.Services;
using Microsoft.AspNetCore.Mvc;

namespace Larder.Controllers;

[ApiController, Route("api/[controller]")]
public class ConsumedFoodsController(IConsumedFoodService consumedFoodService) : Controller
{
    private readonly IConsumedFoodService _consFoodService = consumedFoodService;

    [HttpGet]
    public async Task<ActionResult<List<DayOfEatingDto>>> Index()
    {
        return await _consFoodService.FoodsOfPastWeek();
    }
}
