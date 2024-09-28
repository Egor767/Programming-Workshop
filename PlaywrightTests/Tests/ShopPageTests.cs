using System.Net.Security;
using System.Security.Authentication;
using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using static Microsoft.Playwright.PlaywrightException;
using static PlaywrightTests.Locators;


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
        await _shopPage.GoToCart();
        var len = await _shopPage.GetCartCount();

        Assert.IsTrue(len == 1);
    }

    [TestMethod]
    public async Task SuccsessfulSortingByNames()
    {
        await _shopPage.GotoAsync();
        await _shopPage.Authorization();
        await _shopPage.SortingProducts(type: "az");
        var sortingList = await _shopPage.GetNamesOfProducts();
        var expectedList = sortingList.OrderByDescending(x => x);

        Assert.IsTrue(expectedList.SequenceEqual(sortingList));
    }
}

