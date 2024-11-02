using Larder.Dtos;
using Larder.Services;
using Microsoft.AspNetCore.Mvc;

namespace Larder.Controllers;

using UnitConversionActionResult
                        = Task<ActionResult<ApiResponse<UnitConversionDto>>>;

public class UnitConversionsController(IUnitConversionService service)
                                                            : AppControllerBase
{
    private readonly IUnitConversionService _service = service;

    [HttpPost]
    public async UnitConversionActionResult Create(UnitConversionDto dto)
    {
        try
        {
            UnitConversionDto unitConv = await _service.CreateUnitConversion(dto);

            return new ApiResponse<UnitConversionDto>(
                unitConv, "Unit conversion created", ApiResponseType.Success);
        }
        catch (ApplicationException e)
        {
            return UnprocessableEntity(FromError(e));
        }
    }

    [HttpPut("{id}")]
    public async UnitConversionActionResult Update(UnitConversionDto dto, string id)
    {
        if (dto.Id == null || dto.Id != id) return BadRequest();

        try
        {
            UnitConversionDto resultDto = await _service.UpdateUnitConversion(dto);
            return new ApiResponse<UnitConversionDto>(
                resultDto, "Unit conversion updated", ApiResponseType.Success);
        }
        catch (ApplicationException e)
        {
            return UnprocessableEntity(FromError(e));
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> Delete(string id)
    {
        try
        {
            await _service.DeleteUnitConversion(id);
            return Ok();
        }
        catch (ApplicationException e)
        {
            return UnprocessableEntity(FromError(e));
        }   
    }
}
