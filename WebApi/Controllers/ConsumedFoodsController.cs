using Larder.Dtos;
using Larder.Services;
using Microsoft.AspNetCore.Mvc;

namespace Larder.Controllers;

[ApiController, Route("api/[controller]")]
public class ConsumedFoodsController(IConsumedFoodService consumedFoodService) : Controller
{
    private readonly IConsumedFoodService _consFoodService = consumedFoodService;

    [HttpPost]
    public async Task<ActionResult> Create(ConsumedFoodDto dto)
    {
        if (dto.Id != null) return BadRequest();

        try
        {
            await _consFoodService.CreateConsumedFood(dto);
            return Ok();
        }
        catch (ApplicationException)
        {
            return UnprocessableEntity();
        }
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> Update(ConsumedFoodDto dto, string id)
    {
        if (dto.Id == null || dto.Id != id) return BadRequest();

        try
        {
            await _consFoodService.UpdateConsumedFood(dto);
            return Ok();
        }
        catch (ApplicationException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            await _consFoodService.DeleteConsumedFood(id);
            return Ok();
        }
        catch (ApplicationException)
        {
            return NotFound();
        }
    }
}
