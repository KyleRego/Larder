using OpenQA.Selenium;

namespace Larder.UITests;

public class RegisterTest : UITestBase
{
    [Test]
    public void TestUserCanRegister()
    {
        IWebElement registerBtn = driver.FindElement(By.Id("register-btn"));

        registerBtn.Click();

        IWebElement emailInput = driver.FindElement(By.Id("email"));
        IWebElement passwordInput = driver.FindElement(By.Id("password"));
        IWebElement submitBtn = driver.FindElement(By.Id("submit-register"));
        
        string email = RandomEmail();
        string password = RandomPassword();

        emailInput.SendKeys(email);
        passwordInput.SendKeys(password);

        submitBtn.Click();

        AssertMessage("Account registered successfully.");
    }

    private static string RandomEmail()
    {
        return $"example-{Guid.NewGuid()}@test.com";
    }

    private static string RandomPassword()
    {
        return Guid.NewGuid().ToString() + "aZ!";
    }
}
