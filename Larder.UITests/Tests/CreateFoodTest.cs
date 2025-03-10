namespace Larder.UITests.Tests;

public class CreateFoodTest : UITestBase
{
    [Test]
    public void TestFoodCanBeCreated()
    {
        string foodItemName = $"Test food rice box {Guid.NewGuid()}";

        _driver.LoginTestUser();

        IWebElement newItemLink = _driver.FindElement(By.LinkText("New item"));
        newItemLink.Click();

        IWebElement isFoodToggle = _driver.FindElement(By.CssSelector("[for='is-nutrition-toggle']"));
        isFoodToggle.Click();

        SendKeysToInput("name-input", foodItemName);
        SendKeysToInput("Quantity-amount-input", "2");
        SendKeysToInput("description-input", "A box of rice and seasoning ");

        // At this point the servings should be 8
        // Skip testing the quantity per serving for now

        SendKeysToInput("calories-input", "190");
        SendKeysToInput("gramsProtein-input", "6");
        SendKeysToInput("gramsTotalFat-input", "0.5");
        SendKeysToInput("gramsSaturatedFat-input", "0.1");
        SendKeysToInput("gramsTransFat-input", "0.2");
        SendKeysToInput("gramsTotalCarbs-input", "41");
        SendKeysToInput("gramsTotalSugars-input", "2");
        SendKeysToInput("gramsDietaryFiber-input", "2.1");
        SendKeysToInput("milligramsSodium-input", "570");
        SendKeysToInput("milligramsCholesterol-input", "50");

        IWebElement submitBtn = _driver.FindElement(By.Id("item-form-submit"));
        submitBtn.Click();

        AssertMessage("Successfully created item");
    }

    private void SendKeysToInput(string inputId, string keys)
    {
        IWebElement input = _driver.FindElement(By.Id(inputId));
        input.Clear();
        input.SendKeys(keys);
    }
}
