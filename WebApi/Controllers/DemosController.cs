using Larder.Dtos;
using Larder.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Larder.Controllers;

public class DemosController(IDemoService demoService) : ApplicationControllerBase
{
    private readonly IDemoService _demoService = demoService;

    [AllowAnonymous, HttpPost]
    public async Task<ActionResult> Create()
    {
        await _demoService.CreateDemo();

        return Ok();
    }
}
