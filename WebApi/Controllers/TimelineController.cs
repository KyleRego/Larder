using Larder.Dtos;
using Larder.Services;
using Microsoft.AspNetCore.Mvc;

namespace Larder.Controllers;

[ApiController, Route("api/[controller]")]
public class TimelineController(ITimelineService timelineService) : Controller
{
    private readonly ITimelineService _timelineService = timelineService;

    [HttpGet]
    public async Task<ActionResult<List<NutritionDayDto>>> Index()
    {
        return await _timelineService.FoodsOfPastWeek();
    }
}
