using Microsoft.AspNetCore.Mvc;

using Larder.Dtos;
using Larder.Services.Interface;
using Larder.Models.SortOptions;
using Larder.Models;

namespace Larder.Controllers;

public class UnitsController(IUnitService service) : CrudControllerBase<UnitDto, Unit>(service)
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

}
