using Larder.Tests.TestData;

namespace Larder.Tests.Repository;

public abstract class MockRepositoryBase
{
    protected readonly string testUserId = TestUser.TestUserId();
}