using Larder.Dtos;
using Larder.Services;
using Microsoft.AspNetCore.Mvc;

namespace Larder.Controllers;

public class UnitConversionsController(IUnitConversionService unitConvService)
                                                : AppControllerBase
{
    private readonly IUnitConversionService _unitConvService = unitConvService;

    [HttpPost]
    public async Task<ActionResult<UnitConversionDto>> Create(UnitConversionDto dto)
    {
        try
        {
            return await _unitConvService.CreateUnitConversion(dto);
        }
        catch (ApplicationException)
        {
            return UnprocessableEntity();
        }
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<UnitConversionDto>> Update(UnitConversionDto dto, string id)
    {
        if (dto.Id == null || dto.Id != id) return BadRequest();

        try
        {
            return await _unitConvService.UpdateUnitConversion(dto);
        }
        catch (ApplicationException)
        {
            return UnprocessableEntity();
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            await _unitConvService.DeleteUnitConversion(id);
        }
        catch (ApplicationException)
        {
            return UnprocessableEntity();
        }

        return Ok();
    }
}
