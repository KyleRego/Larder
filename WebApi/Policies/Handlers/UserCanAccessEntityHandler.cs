using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;

using Larder.Policies.Requirements;
using Larder.Models;

namespace Larder.Policies.Handlers;

public class UserCanAccessEntityHandler
        : AuthorizationHandler<UserCanAccessEntityRequirement, EntityBase>
{
    protected override Task HandleRequirementAsync(
                            AuthorizationHandlerContext context,
                            UserCanAccessEntityRequirement requirement,
                                            EntityBase resource)
    {
        string? userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId != null && userId == resource.UserId)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;        
    }
}
