using Larder.UITests.Extension;
using OpenQA.Selenium;

namespace Larder.UITests;

public class CreateItemTest : UITestBase
{
    [Test]
    public void TestFoodCanBeCreated()
    {
        driver.LoginTestUser();

        IWebElement newItemLink = driver.FindElement(By.LinkText("New item"));
        newItemLink.Click();

        IWebElement nameInput = driver.FindElement(By.Id("nameInput"));
        nameInput.SendKeys("New item");

        IWebElement amountInput = driver.FindElement(By.Id("amountInput"));
        amountInput.SendKeys("2");

        IWebElement submitBtn = driver.FindElement(By.Id("submit-new-item"));
        submitBtn.Click();

        AssertMessage("Item created");
    }
}
