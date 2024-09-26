using Microsoft.Playwright.MSTest;
using static Microsoft.Playwright.PlaywrightException;


namespace PlaywrightTests;

[TestClass]
public class ShopPageTests: PageTest
{
    private ShopPage _shopPage;

    [TestInitialize]
    public void NewPage()
    {
        _shopPage = new ShopPage(Page);
    }

    [TestMethod]
    public async Task AddSingleProductTest()
    {
        await _shopPage.GotoAsync();
        await _shopPage.Authorization();
        await _shopPage.AddSingleProductToCart();
        var expected = "1";

        await Expect(_shopPage.GetCartCounter()).ToContainTextAsync(expected);
    }

    [TestMethod]
    public async Task RemoveProductFromCart()
    {
        await _shopPage.GotoAsync();
        await _shopPage.Authorization();
        await _shopPage.AddSingleProductToCart();
        await _shopPage.RemoveProductFromCart();

        //await Assert.ThrowsExceptionAsync<Microsoft.Playwright.PlaywrightException>(async () => await _shopPage.GetCartCounter());
    }
}