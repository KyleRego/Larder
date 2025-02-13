using System.Security.Claims;
using Larder.Models;
using Larder.Policies.Requirements;
using Larder.Services;
using Larder.Tests.TestData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Larder.Tests.Services;

public abstract class ServiceTestsBase
{
    protected readonly Mock<IServiceProviderWrapper> _serviceProvider;
    protected static readonly string testUserId = TestUser.TestUserId();
    protected readonly Claim testUserClaim = TestUser.TestUserClaim();

    public ServiceTestsBase()
    {
        _serviceProvider = new Mock<IServiceProviderWrapper>();

        var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        var mockAuthorizationService = new Mock<IAuthorizationService>();
        var mockHttpContext = new Mock<HttpContext>();
        var mockClaimsPrincipal = new Mock<ClaimsPrincipal>();

        mockHttpContextAccessor.Setup(_ => _.HttpContext)
                                            .Returns(mockHttpContext.Object);
        mockHttpContext.Setup(_ => _.User).Returns(mockClaimsPrincipal.Object);
        mockClaimsPrincipal.Setup(_ => _.FindFirst(ClaimTypes.NameIdentifier))
                                                    .Returns(testUserClaim);

        mockAuthorizationService.Setup(_ =>
                                _.AuthorizeAsync(mockClaimsPrincipal.Object,
                                                        It.IsAny<EntityBase>(),
                                        UserCanAccessEntityRequirement.Name))
                                .ReturnsAsync(AuthorizationResult.Success());

        _serviceProvider.Setup(_ => _.GetRequiredService<IHttpContextAccessor>())
                                    .Returns(mockHttpContextAccessor.Object);
        _serviceProvider.Setup(_ => _.GetRequiredService<IAuthorizationService>())
                                    .Returns(mockAuthorizationService.Object);
    }
}
