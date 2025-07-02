using System.Security.Claims;

namespace Larder.Services.Impl;

public abstract class AppServiceBase(IServiceProviderWrapper serviceProvider)
{
    private readonly IHttpContextAccessor _httpContextAccessor
                = serviceProvider.GetRequiredService<IHttpContextAccessor>();

    protected string CurrentUserId()
    {
        return _httpContextAccessor.HttpContext?.User?
                            .FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? throw new ApplicationException("Current user id is missing");
    }

    protected void RestrictAccessToMe()
    {
        string? email = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;

        if (email != "regoky@outlook.com") throw new ApplicationException("This feature is restricted");
    }
}
