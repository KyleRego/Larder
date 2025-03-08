namespace Larder.UITests.Tests;

public class UnitCrudTest : UITestBase
{
    [Test]
    public void PerformTest()
    {
        _driver.LoginTestUser();
        _driver.ClickNewSomethingDropdown();
        _driver.ClickLinkByText("New unit");
        _driver.FillTextInput("name", "Cups - test");
        _driver.SelectOptionByText("type", "Volume");
        _driver.ClickButtonByText("Create unit");
        AssertMessage("Successfully created unit");

        // TODO Finish this test
    }
}
