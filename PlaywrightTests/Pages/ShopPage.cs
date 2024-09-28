using Microsoft.Playwright;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using static PlaywrightTests.Locators;

namespace PlaywrightTests;

public class ShopPage: Page
{
    public ShopPage(IPage page): base(page) {}
    
    public async Task GoToCart()
    {
        await _page.Locator(GoToCartButton).ClickAsync();
    }

    public async Task AddSingleProductToCart()
    {
        await _page.Locator(AddToCartProduct).ClickAsync();
    }

    public async Task RemoveProductFromCart()
    {
        await _page.Locator(RemoveProduct).ClickAsync();
    }
    
    public async Task<int> GetCartCount()
    {
        var len = await _page.Locator(CartList).CountAsync();
        return len;
    }

    public ILocator GetCartCounter()
    {
        return _page.Locator(CartCounter);
    }

    public async Task SortingProducts(string type)
    {
        await _page.Locator(SortContainer).SelectOptionAsync(new[] {type});
    }

    public async Task<string[]> GetNamesOfProducts()
    {
        var length = await _page.Locator(ProductName).CountAsync();
        var result = new List<string>();
        for (int i=0; i<length; i++)
        {
            result.Add(await _page.Locator(ProductName).Nth(i).InnerTextAsync());
        }

        return result.ToArray();
    }
}

