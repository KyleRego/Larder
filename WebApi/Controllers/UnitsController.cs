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
    public async Task<List<Unit>> Index(string? sortOrder)
    {
        if (sortOrder == "Name_Desc")
        {
            return await _repository.GetUnits("Name_Desc");
        }
        else
        {
            return await _repository.GetUnits("Name");
        }
        
    }
}