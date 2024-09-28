using Microsoft.Playwright;
using System.Threading.Tasks;
using static PlaywrightTests.Locators;

namespace PlaywrightTests;

public class CartPage: Page{
    public CartPage(IPage page): base(page) {}

    public async Task AddSingleProductToCart()
    {
        await _page.Locator(AddToCartProduct).ClickAsync();
    }
    public async Task GoToCart()
    {
        await _page.Locator(GoToCartButton).ClickAsync();
    }
    public async Task Checkout()
    {
        await _page.Locator(GoToCheckout).ClickAsync();
    }
    public async Task FillFirstName(string FirstName)
    {
        await _page.Locator(FirstNameCart).FillAsync(FirstName);
    }
    public async Task FillSecondName(string LastName)
    {
        await _page.Locator(LastNameCart).FillAsync(LastName);
    }
    public async Task FillPostalCode(string PostalCode)
    {
        await _page.Locator(PostalCodeCart).FillAsync(PostalCode);
    }
    public async Task ClickContinueButton()
    {
        await _page.Locator(ContinueButton).ClickAsync();
    }
    public async Task ClickFinishButton()
    {
        await _page.Locator(FinishButton).ClickAsync();
    }
    public ILocator GetErrorMessage()
    {
        return _page.Locator(ErrorMessage);
    }
    public ILocator GetTitle()
    {
        return _page.Locator(Title);
    }
}