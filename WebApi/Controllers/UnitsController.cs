using Larder.Models;
using Larder.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Matching;

namespace Larder.Controllers;

[ApiController]
[Route("api/{controller}")]
public class UnitsController(IUnitRepository repository) : ControllerBase
{
    private readonly IUnitRepository _repository = repository;

    [HttpGet]
    public async Task<List<Unit>> Index(string? sortOrder)
    {
        if (sortOrder == null) return await _repository.GetUnits(UnitsSortOrder.Name);

        try
        {
            UnitsSortOrder sortOrderEnum = (UnitsSortOrder)Enum.Parse(typeof(UnitsSortOrder), sortOrder, true);
            return await _repository.GetUnits(sortOrderEnum);
        }
        catch (ArgumentException)
        {
            return await _repository.GetUnits(UnitsSortOrder.Name);
        }
    }
}