using Larder.Tests.TestData;

namespace Larder.Tests.Mocks.Repository;

public abstract class MockRepositoryBase
{
    protected readonly string testUserId = TestUser.TestUserId();
}
