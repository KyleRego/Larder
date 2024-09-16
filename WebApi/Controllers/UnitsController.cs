using Microsoft.AspNetCore.Mvc;

using Larder.Repository;
using Larder.Dtos;
using Larder.Services;
using Microsoft.AspNetCore.Authorization;

namespace Larder.Controllers;

[ApiController, Route("api/[controller]")]
public class UnitsController(IUnitService service) : ControllerBase
{
    private readonly IUnitService _service = service;

    [HttpGet, Authorize]
    public async Task<List<UnitDto>> Index(string? sortOrder, string? search)
    {
        if (sortOrder != null && Enum.TryParse(sortOrder, out UnitSortOptions sortBy))
        {
            return await _service.GetUnits(sortBy, search);
        }
        else
        {
            return await _service.GetUnits(UnitSortOptions.AnyOrder, search);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UnitDto>> Show(string id)
    {
        UnitDto? result = await _service.GetUnit(id);

        return (result == null) ? NotFound() : result;
    }

    [HttpPost]
    public async Task<ActionResult<UnitDto>> Create(UnitDto dto)
    {
        try
        {
            return await _service.CreateUnit(dto);
        }
        catch (ApplicationException)
        {
            return UnprocessableEntity();
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UnitDto>> Update(string id, UnitDto dto)
    {
        if (dto.Id == null || dto.Id != id) return BadRequest();

        try
        {
            return await _service.UpdateUnit(dto);
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
            await _service.DeleteUnit(id);
        }
        catch (ApplicationException)
        {
            return UnprocessableEntity();
        }

        return Ok();
    }
}
