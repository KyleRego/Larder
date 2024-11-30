using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;

namespace Larder.UITests.Extension;

public static class ChromeDriverExtension
{
    public static void LoginTestUser(this IWebDriver driver)
    {
        IConfiguration config = ConfigurationLoader.LoadConfiguration();

        string username = config["TestUser:UserName"]
            ?? throw new ApplicationException("Test user username is missing");

        string password = config["TestUser:Password"]
            ?? throw new ApplicationException("Test user password is missing");

        IWebElement loginBtn = driver.FindElement(By.Id("login-btn"));
        loginBtn.Click();

        IWebElement emailInput = driver.FindElement(By.Id("email"));
        IWebElement passwordInput = driver.FindElement(By.Id("password"));
        IWebElement submitBtn = driver.FindElement(By.Id("submit-login"));

        emailInput.SendKeys(username);
        passwordInput.SendKeys(password);

        submitBtn.Click();

        // TODO: Refactor to use an ID selector here
        IWebElement shooMessageBtn = driver.FindElement(By.CssSelector(".btn-outline-success"));
        shooMessageBtn.Click();
    }
}
