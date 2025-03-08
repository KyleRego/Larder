using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Support.UI;

namespace Larder.UITests.Extension;

public static class ChromeDriverExtension
{
    public static void LoginTestUser(this IWebDriver driver, bool shooMessage = true)
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

        if (shooMessage == true)
        {
            // TODO: Refactor to use an ID selector here
            IWebElement shooMessageBtn = driver.FindElement(By.CssSelector(".btn-outline-success"));
            shooMessageBtn.Click();
        }
    }

    public static void ClickNewSomethingDropdown(this IWebDriver driver)
    {
        IWebElement dropdown;
        try
        {
            dropdown = driver.FindElement(By.CssSelector(".d-none.d-lg-block #new-something-dropdown"));
        }
        catch
        {
            dropdown = driver.FindElement(By.CssSelector(".d-lg-none #new-something-dropdown"));
        }
        dropdown.Click();
    }

    public static void ClickLinkByText(this IWebDriver driver, string text)
    {
        IWebElement link = driver.FindElement(By.LinkText(text));
        link.Click();
    }

    public static void FillTextInput(
        this IWebDriver driver, string inputName, string keys)
    {
        IWebElement input = driver.FindElement(By.CssSelector($"input[name='{inputName}']"));
        input.SendKeys(keys);
    }

    public static void SelectOptionByText(
        this IWebDriver driver, string selectName, string optionText)
    {
        IWebElement selectElement =
            driver.FindElement(By.CssSelector($"select[name='{selectName}']"));
        SelectElement select = new(selectElement);
        select.SelectByText(optionText);
    }

    public static void ClickButtonByText(this IWebDriver driver, string buttonText)
    {
        var buttons = driver.FindElements(By.TagName("button"));
        foreach (IWebElement button in buttons)
        {
            if (button.Text.Trim() == buttonText)
            {
                button.Click();
                return;
            }
        }
    }

}
