using Microsoft.AspNetCore.Mvc;

using Larder.Repository;
using Larder.Dtos;
using Larder.Services.Interface;

namespace Larder.Controllers;

public class UnitsController(IUnitService service) : AppControllerBase
{
    private readonly IUnitService _service = service;

    [HttpGet]
    public async Task<ActionResult<List<UnitDto>>>
                                    Index(string? sortOrder, string? search)
    {
        try
        {
            if (sortOrder != null && Enum.TryParse(sortOrder,
                                                out UnitSortOptions sortBy))
            {
                return await _service.GetUnits(sortBy, search);
            }
            else
            {
                return await _service.GetUnits(UnitSortOptions.AnyOrder,
                                                                    search);
            }
        }
        catch (ApplicationException)
        {
            return UnprocessableEntity();
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UnitDto>> Show(string id)
    {
        UnitDto? result = await _service.GetUnit(id);

        return (result == null) ? NotFound() : result;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<UnitDto>>> Create(UnitDto dto)
    {
        try
        {
            UnitDto unit = await _service.CreateUnit(dto);
            return new ApiResponse<UnitDto>(unit, "Unit created",
                                                    ApiResponseType.Success);
        }
        catch (ApplicationException e)
        {
            return UnprocessableEntity(FromError(e));
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<UnitDto>>>
                                            Update(string id, UnitDto dto)
    {
        if (dto.Id == null || dto.Id != id) return BadRequest();

        try
        {
            UnitDto resultDto = await _service.UpdateUnit(dto);
            return new ApiResponse<UnitDto>(resultDto, "Unit updated",
                                                ApiResponseType.Success);
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
            await _service.DeleteUnit(id);

            return Ok(
                new ApiResponse<object>("Unit deleted",
                                            ApiResponseType.Success)
            );
        }
        catch (ApplicationException e)
        {
            return UnprocessableEntity(FromError(e));
        }
    }
}
