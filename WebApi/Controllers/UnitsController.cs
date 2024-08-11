using Microsoft.AspNetCore.Mvc;

using Larder.Repository;
using Larder.Dtos;
using Larder.Services;

namespace Larder.Controllers;

[ApiController, Route("api/[controller]")]
public class UnitsController(IUnitService service) : ControllerBase
{
    private readonly IUnitService _service = service;

    [HttpGet]
    public async Task<List<UnitDto>> Index(string? sortOrder)
    {
        if (sortOrder != null && Enum.TryParse(sortOrder, out UnitSortOptions sortBy))
        {
            return await _service.GetUnits(sortBy, null);
        }
        else
        {
            return await _service.GetUnits(UnitSortOptions.AnyOrder, null);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UnitDto>> Show(string id)
    {
        UnitDto? result = await _service.GetUnit(id);

        return (result == null) ? NotFound() : result;
    }

    [HttpPost]
    public async Task<ActionResult> Create(UnitDto dto)
    {
        try
        {
            await _service.CreateUnit(dto);
        }
        catch (ApplicationException)
        {
            return UnprocessableEntity();
        }

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(string id, UnitDto dto)
    {
        if (dto.Id == null || dto.Id != id) return BadRequest();

        try
        {
            await _service.UpdateUnit(dto);
        }
        catch (ApplicationException)
        {
            return UnprocessableEntity();
        }

        return Ok();
    }
}
