namespace Larder.UITests.Tests;

public class CreateItemTest : UITestBase
{
    [Test]
    public void TestItemCanBeCreated()
    {
        _driver.LoginTestUser();

        IWebElement newItemLink = _driver.FindElement(By.LinkText("New item"));
        newItemLink.Click();

        IWebElement nameInput = _driver.FindElement(By.Id("name-input"));
        nameInput.SendKeys("New item");

        IWebElement amountInput = _driver.FindElement(By.Id("Quantity-amount-input"));
        amountInput.SendKeys("2");

        IWebElement submitBtn = _driver.FindElement(By.Id("item-form-submit"));
        submitBtn.Click();

        AssertMessage("Successfully created record");
    }
}
