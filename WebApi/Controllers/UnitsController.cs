using Larder.Models;
using Larder.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Larder.Controllers;

[ApiController]
[Route("api/{controller}")]
public class UnitsController(IUnitRepository repository) : ControllerBase
{
    private readonly IUnitRepository _repository = repository;

    [HttpGet]
    public async Task<List<Unit>> Index()
    {
        return await _repository.GetUnits();
    }
}