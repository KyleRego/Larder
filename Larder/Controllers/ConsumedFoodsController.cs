using Larder.Dtos;
using Larder.Services;
using Microsoft.AspNetCore.Mvc;

namespace Larder.Controllers;

public class ConsumedFoodsController(IConsumedFoodService consumedFoodService)
                                        : AppControllerBase
{
    private readonly IConsumedFoodService _consFoodService =
                                                         consumedFoodService;

    [HttpPost]
    public async Task<ActionResult<ConsumedFoodDto>>
                                                    Create(ConsumedFoodDto dto)
    {
        if (dto.Id != null) return BadRequest();

        try
        {
            return await _consFoodService.CreateConsumedFood(dto);
        }
        catch (ApplicationException)
        {
            return UnprocessableEntity();
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ConsumedFoodDto>>
                                        Update(ConsumedFoodDto dto, string id)
    {
        if (dto.Id == null || dto.Id != id) return BadRequest();

        try
        {
            return await _consFoodService.UpdateConsumedFood(dto);
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
