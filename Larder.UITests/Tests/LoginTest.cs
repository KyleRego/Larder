namespace Larder.UITests.Tests;

public class LoginTest : UITestBase
{
    [Test]
    public void TestUserCanLogin()
    {
        driver.LoginTestUser(false);

        AssertMessage("You are now logged in.");
    }
}
