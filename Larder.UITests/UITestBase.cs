using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Larder.UITests;

[TestFixture]
public abstract class UITestBase
{
    protected IWebDriver driver;
    protected string clientURL;

    public UITestBase()
    {
        IConfiguration config = ConfigurationLoader.LoadConfiguration();
        clientURL = config["ClientURL"]
            ?? throw new InvalidOperationException(
                                        "ClientURL is missing in config");

        driver = new ChromeDriver();
    }

    [TearDown]
    public void TearDown()
    {
        driver.Close();
    }
    
    [OneTimeTearDown]
    public void Dispose()
    {
        driver.Dispose();
    }

    [SetUp]
    public void SetUp()
    {
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0.5);
        driver.Manage().Window.Maximize();
        driver.Navigate().GoToUrl(clientURL);
    }

    protected void AssertMessage(string text)
    {
        IWebElement message = driver.FindElement(By.Id("message-text"));
        Assert.That(text, Is.EqualTo(message.Text));
    }
}
