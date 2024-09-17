using Microsoft.AspNetCore.Authorization;

namespace Larder.Policies.Requirements;

public class UserCanAccessEntityRequirement : IAuthorizationRequirement
{
    public static readonly string Name = "UserCanAccessEntity";
}
