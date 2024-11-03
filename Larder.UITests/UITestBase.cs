using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Larder.UITests;

[TestFixture]
public abstract class UITestBase
{
    protected IWebDriver driver;
    protected string clientURL;

    [SetUp]
    public void SetUp()
    {
        IConfiguration config = ConfigurationLoader.LoadConfiguration();
        clientURL = config["ClientURL"]
            ?? throw new InvalidOperationException("No client URL");

        driver = new ChromeDriver();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
        driver.Manage().Window.Maximize();

        driver.Navigate().GoToUrl(clientURL);
    }

    [TearDown]
    public void TearDown()
    {
        driver.Close();
    }

    protected void AssertMessage(string text)
    {
        IWebElement message = driver.FindElement(By.Id("message-text"));
        Assert.That(text, Is.EqualTo(message.Text));
    }
}
