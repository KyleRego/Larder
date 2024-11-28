using Larder.Dtos;
using Larder.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Larder.Controllers;

public class DemosController(IDemoService demoService) : AppControllerBase
{
    private readonly IDemoService _demoService = demoService;

    [AllowAnonymous, HttpPost]
    public async Task<ActionResult> Create()
    {
        try
        {
            await _demoService.CreateDemo();
        }
        catch (ApplicationException)
        {
            return UnprocessableEntity();
        }

        return Ok();
    }
}
