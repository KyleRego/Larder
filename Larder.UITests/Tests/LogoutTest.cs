namespace Larder.UITests.Tests;

public class LogoutTest : UITestBase
{
    [Test]
    public void TestUserCanLogout()
    {
        _driver.LoginTestUser();

        try
        {
            IWebElement logoutBtn = _driver.FindElement(By.CssSelector(".d-none.d-lg-block #logout-btn"));
            logoutBtn.Click();
        } catch
        {
            IWebElement logoutBtn = _driver.FindElement(By.CssSelector(".d-lg-none #logout-btn"));
            logoutBtn.Click();  
        }

        AssertMessage("You are now logged out.");
    }
}
