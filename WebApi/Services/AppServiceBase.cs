using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Larder.Services;

public abstract class AppServiceBase(IServiceProviderWrapper serviceProvider)
{
    private readonly IHttpContextAccessor _httpContextAccessor
                = serviceProvider.GetRequiredService<IHttpContextAccessor>();
    private readonly IAuthorizationService _authzService
                = serviceProvider.GetRequiredService<IAuthorizationService>();

    protected string CurrentUserId()
    {
        return _httpContextAccessor.HttpContext?.User?
                            .FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? throw new ApplicationException("Current user id is missing");
    }
}
