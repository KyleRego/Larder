namespace Larder.UITests.Tests;

public class CreateFoodTest : UITestBase
{
    [Test]
    public void TestFoodCanBeCreated()
    {
        string foodItemName = $"Test food rice box {Guid.NewGuid()}";

        driver.LoginTestUser();

        IWebElement newItemLink = driver.FindElement(By.LinkText("New item"));
        newItemLink.Click();

        IWebElement isFoodToggle = driver.FindElement(By.CssSelector("[for='is-food-toggle']"));
        isFoodToggle.Click();

        SendKeysToInput("name-input", foodItemName);
        SendKeysToInput("amount-input", "2");
        SendKeysToInput("description-input", "A box of rice and seasoning ");
        SendKeysToInput("servings-per-item-input", "4");

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

        IWebElement submitBtn = driver.FindElement(By.Id("submit-new-item"));
        submitBtn.Click();

        AssertMessage("Item created");

        IWebElement foodsBtn = driver.FindElement(By.LinkText("Foods"));
        foodsBtn.Click();
    }

    private void SendKeysToInput(string inputId, string keys)
    {
        IWebElement input = driver.FindElement(By.Id(inputId));
        input.Clear();
        input.SendKeys(keys);
    }
}
