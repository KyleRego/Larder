using System.Security.Claims;
using Larder.Models;
using Larder.Policies.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace Larder.Services;

public abstract class ApplicationServiceBase(IHttpContextAccessor httpConAcsr,
                                            IAuthorizationService authService)
{
    private readonly IHttpContextAccessor _httpConAcsr = httpConAcsr;
    private readonly IAuthorizationService _authService = authService;

    protected string CurrentUserId()
    {
        return _httpConAcsr.HttpContext?.User?
                            .FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new ApplicationException("No user id in the HTTP context");
    }

    protected async Task ThrowIfUserCannotAccess(EntityBase resource)
    {
        ClaimsPrincipal user = _httpConAcsr.HttpContext?.User
            ?? throw new ApplicationException("No claims principal/ user");

        AuthorizationResult authorizationResult =
            await _authService.AuthorizeAsync(user, resource, UserCanAccessEntityRequirement.Name);

        if (!authorizationResult.Succeeded)
            throw new ApplicationException("Authorization did not succeed");
    }
}
