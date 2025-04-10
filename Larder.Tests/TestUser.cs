using System.Security.Claims;

namespace Larder.Tests;

public static class TestUserData
{
    private static readonly string _testUserId
                = Guid.NewGuid().ToString();
    private static readonly Claim _testUserClaim
        = new(ClaimTypes.NameIdentifier, TestUserId());

    public static string TestUserId()
    {
        return _testUserId;
    }

    public static Claim TestUserClaim()
    {
        return _testUserClaim;
    }
}
