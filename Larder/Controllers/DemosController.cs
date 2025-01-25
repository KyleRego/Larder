using Larder.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Larder.Controllers;

public class DemosController(IDemoService demoService) : AppControllerBase
{
    private readonly IDemoService _demoService = demoService;

    [AllowAnonymous, HttpPost]
    public async Task<ActionResult<ApiResponse<object>>> Create()
    {
        try
        {
            await _demoService.CreateDemo();
            return new ApiResponse<object>(
                "Demo set up successfully! Thank you for trying Larder 🤍 it is a work in progress!",
                                                ApiResponseType.Success);
        }
        catch (ApplicationException e)
        {
            return UnprocessableEntity(FromError(e));
        }
    }
}
