using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Larder.UITests.Tests;

public class ItemCrudTest : UITestBase
{
    [Test]
    public void TestItemCanBeCreatedAndUpdated()
    {
        _driver.LoginTestUser();

        string itemName = $"New item {Guid.NewGuid()}";

        IWebElement newItemLink = _driver.FindElement(By.LinkText("New item"));
        newItemLink.Click();

        IWebElement nameInput = _driver.FindElement(By.Id("name-input"));
        nameInput.SendKeys("New item");

        IWebElement amountInput = _driver.FindElement(By.Id("Quantity-amount-input"));
        amountInput.SendKeys("2");

        IWebElement submitBtn = _driver.FindElement(By.Id("item-form-submit"));
        submitBtn.Click();

        AssertMessage("Successfully created item");

        NavigateToItemPage(itemName);

        Console.ReadLine();

        _driver.ClickLinkByText("Edit item");

        _driver.FillTextInput("name-input", $"{itemName} edited");

        _driver.FindElement(By.Id("item-form-submit")).Click();

        AssertMessage("Updated item successfully");
    }

    private void NavigateToItemPage(string itemName)
    {
        WebDriverWait wait = new(_driver, TimeSpan.FromSeconds(10));
        var rows = _driver.FindElements(By.CssSelector("tbody tr"));

        foreach (var row in rows)
        {
            var ths = row.FindElements(By.CssSelector("th"));

            foreach (var th in ths)
            {
                if (th.Text.Trim() == itemName)
                {
                    ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", row);
                    wait.Until(ExpectedConditions.ElementToBeClickable(row));
                    ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", row);
                    return;
                }
            }
        }
    }
}
