using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Larder.Controllers;

[ApiController, Authorize, Route("api/[controller]")]
public abstract class AppControllerBase : Controller
{
    
}
