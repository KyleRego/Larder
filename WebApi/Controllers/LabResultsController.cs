using Microsoft.AspNetCore.Mvc;

using Larder.Dtos;
using Larder.Services;

namespace Larder.Controllers;

public class LabResultsController(ILabResultsService service)
                                                : ApplicationControllerBase
{
    private readonly ILabResultsService _service = service;

    [HttpGet]
    public async Task<ActionResult<List<LabResult>>> Index()
    {
        try
        {
            return await _service.GetLabResults();
        }
        catch (ApplicationException)
        {
            return UnprocessableEntity();
        }
    }
}
