using Microsoft.Playwright;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using static PlaywrightTests.Locators;

namespace PlaywrightTests;

public class ShopPage: Page
{
    public ShopPage(IPage page): base(page) {}
    public async Task Authorization()
    {
        await _page.Locator(Username).FillAsync("standard_user");
        await _page.Locator(Password).FillAsync("secret_sauce");
        await _page.Locator(LoginButton).ClickAsync();
    }
    public async Task AddSingleProductToCart()
    {
        await _page.Locator(AddToCartProduct).ClickAsync();
    }

    public async Task RemoveProductFromCart()
    {
        await _page.Locator(RemoveProduct).ClickAsync();
    }

    public ILocator GetCartCounter()
    {
        return _page.Locator(CartCounter);
    }

    public async Task SortingProducts(string type = "az")
    {
        await _page.Locator(SortContainer).SelectOptionAsync(new[] {type});
    }
    public async Task<string[]> GetNamesOfProducts()
    {
        var length = _page.Locator(InventoryList).CountAsync();
        var result = new List<string>();

        return result.ToArray();
    }

}