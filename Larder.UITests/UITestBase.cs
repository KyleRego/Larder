using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Larder.UITests;

[TestFixture]
public abstract class UITestBase
{
    protected IWebDriver _driver;
    protected string clientURL;

    public UITestBase()
    {
        IConfiguration config = ConfigurationLoader.LoadConfiguration();
        clientURL = config["ClientURL"]
            ?? throw new InvalidOperationException(
                                        "ClientURL is missing in config");
    }

    [OneTimeSetUp]
    public void Setup()
    {
        new DriverManager().SetUpDriver(new ChromeConfig());
        _driver = new ChromeDriver();
    }

    [SetUp]
    public void SetUp()
    {
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0.5);
        _driver.Manage().Window.Maximize();
        _driver.Navigate().GoToUrl(clientURL);
    }

    [TearDown]
    public void TearDown()
    {
        _driver.Close();
    }
    
    [OneTimeTearDown]
    public void Dispose()
    {
        _driver.Dispose();
    }

    protected void AssertMessage(string text)
    {
        By selector = By.Id("message-text");
        WebDriverWait wait = new(_driver, TimeSpan.FromSeconds(10));
        wait.Until(d => d.FindElement(selector).Displayed);
        IWebElement message = _driver.FindElement(selector);
        Assert.That(text, Is.EqualTo(message.Text));
    }
}
