using System.Security.Claims;
using Larder.Models;
using Larder.Policies.Requirements;
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

    protected async Task ThrowIfUserCannotAccess(EntityBase resource)
    {
        ClaimsPrincipal user = _httpContextAccessor.HttpContext?.User
            ?? throw new ApplicationException("No claims principal/ user");

        AuthorizationResult authorizationResult =
                            await _authzService.AuthorizeAsync(user, resource,
                                        UserCanAccessEntityRequirement.Name);

        if (!authorizationResult.Succeeded)
            throw new ApplicationException("Authorization did not succeed");
    }
}
