using Microsoft.AspNetCore.Mvc;

using Larder.Models;
using Larder.Repository;

namespace Larder.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UnitsController(IUnitRepository repository) : ControllerBase
{
    private readonly IUnitRepository _repository = repository;

    [HttpGet]
    public async Task<List<Unit>> Index(string? sortOrder)
    {
        if (sortOrder != null && Enum.TryParse(sortOrder, out UnitSortOptions sortBy))
        {
            return await _repository.GetAll(sortBy, null);
        }
        else
        {
            return await _repository.GetAll(UnitSortOptions.AnyOrder, null);
        }
    }
}