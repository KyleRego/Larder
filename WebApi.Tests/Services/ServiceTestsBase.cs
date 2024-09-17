using System.Security.Claims;
using Larder.Models;
using Larder.Policies.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Larder.Tests.Services;

public abstract class ServiceTestsBase
{
    protected readonly string mockUserId = Guid.NewGuid().ToString();
    protected readonly Mock<IHttpContextAccessor> mockHttpContextAccessor;
    protected readonly Mock<IAuthorizationService> mockAuthorizationService;

    public ServiceTestsBase()
    {
        mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        var mockHttpContext = new Mock<HttpContext>();
        mockHttpContextAccessor.Setup(_ => _.HttpContext).Returns(mockHttpContext.Object);

        var mockClaimsPrincipal = new Mock<ClaimsPrincipal>();
        mockHttpContext.Setup(_ => _.User).Returns(mockClaimsPrincipal.Object);

        var claim = new Claim(ClaimTypes.NameIdentifier, mockUserId);

        mockClaimsPrincipal.Setup(_ => _.FindFirst(ClaimTypes.NameIdentifier)).Returns(claim);

        mockAuthorizationService = new Mock<IAuthorizationService>();

        mockAuthorizationService.Setup(_ => _.AuthorizeAsync(mockClaimsPrincipal.Object,
                                                    It.IsAny<EntityBase>(),
                                                    UserCanAccessEntityRequirement.Name))
                                    .ReturnsAsync(AuthorizationResult.Success());
    }
}
