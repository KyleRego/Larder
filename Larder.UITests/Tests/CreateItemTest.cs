namespace Larder.UITests.Tests;

public class CreateItemTest : UITestBase
{
    [Test]
    public void TestItemCanBeCreated()
    {
        driver.LoginTestUser();

        IWebElement newItemLink = driver.FindElement(By.LinkText("New item"));
        newItemLink.Click();

        IWebElement nameInput = driver.FindElement(By.Id("name-input"));
        nameInput.SendKeys("New item");

        IWebElement amountInput = driver.FindElement(By.Id("Quantity-amount-input"));
        amountInput.SendKeys("2");

        IWebElement submitBtn = driver.FindElement(By.Id("item-form-submit"));
        submitBtn.Click();

        AssertMessage("Item created");
    }
}
