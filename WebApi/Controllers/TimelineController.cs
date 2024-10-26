using Larder.Dtos;
using Larder.Services;
using Microsoft.AspNetCore.Mvc;

namespace Larder.Controllers;

public class TimelineController(ITimelineService timelineService)
                                        : AppControllerBase
{
    private readonly ITimelineService _timelineService = timelineService;

    [HttpGet]
    public async Task<ActionResult<List<NutritionDayDto>>> Index()
    {
        return await _timelineService.FoodsOfPastWeek();
    }
}
