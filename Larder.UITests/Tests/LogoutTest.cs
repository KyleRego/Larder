namespace Larder.UITests.Tests;

public class LogoutTest : UITestBase
{
    [Test]
    public void TestUserCanLogout()
    {
        driver.LoginTestUser();

        try
        {
            IWebElement logoutBtn = driver.FindElement(By.CssSelector(".d-none.d-lg-block #logout-btn"));
            logoutBtn.Click();
        } catch
        {
            IWebElement logoutBtn = driver.FindElement(By.CssSelector(".d-lg-none #logout-btn"));
            logoutBtn.Click();  
        }

        AssertMessage("You are now logged out.");
    }
}
