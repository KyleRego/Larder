using System.Security.Claims;
using Larder.Models;
using Larder.Policies.Requirements;
using Larder.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Larder.Tests.Services;

public abstract class ServiceTestsBase
{
    protected readonly Mock<IServiceProviderWrapper> mSP;
    protected static readonly string mockUserId = Guid.NewGuid().ToString();
    protected readonly Claim mockUserClaim = new(ClaimTypes.NameIdentifier,
                                                                mockUserId);

    public ServiceTestsBase()
    {
        mSP = new Mock<IServiceProviderWrapper>();

        var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        var mockAuthorizationService = new Mock<IAuthorizationService>();
        var mockHttpContext = new Mock<HttpContext>();
        var mockClaimsPrincipal = new Mock<ClaimsPrincipal>();

        mockHttpContextAccessor.Setup(_ => _.HttpContext)
                                            .Returns(mockHttpContext.Object);
        mockHttpContext.Setup(_ => _.User).Returns(mockClaimsPrincipal.Object);
        mockClaimsPrincipal.Setup(_ => _.FindFirst(ClaimTypes.NameIdentifier))
                                                    .Returns(mockUserClaim);

        mockAuthorizationService.Setup(_ =>
                                _.AuthorizeAsync(mockClaimsPrincipal.Object,
                                                        It.IsAny<EntityBase>(),
                                        UserCanAccessEntityRequirement.Name))
                                .ReturnsAsync(AuthorizationResult.Success());

        mSP.Setup(_ => _.GetRequiredService<IHttpContextAccessor>())
                                    .Returns(mockHttpContextAccessor.Object);
        mSP.Setup(_ => _.GetRequiredService<IAuthorizationService>())
                                    .Returns(mockAuthorizationService.Object);
    }
}
