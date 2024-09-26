using Microsoft.Playwright.MSTest;

namespace PlaywrightTests;

[TestClass]
public class CartPageTests: PageTest
{
    private CartPage _cartPage;

    [TestInitialize]
    public void NewPage()
    {
        _cartPage = new CartPage(Page);
    }

    [TestMethod]
    public async Task WrongFillForm()
    {
        await _cartPage.Authorization();
        await _cartPage.AddSingleProductToCart();
        await _cartPage.GoToCart();
        await _cartPage.Checkout();
        await _cartPage.FillFirstName("First Name");
        await _cartPage.FillSecondName("Second Name");
        await _cartPage.FillPostalCode("");
        var expectMessage = "Error: Postal Code is required";

        await Expect(_cartPage.GetErrorMessage()).ToContainTextAsync(expectMessage);
    }

    [TestMethod]
    public async Task SuccessfulFillForm()
    {
        await _cartPage.Authorization();
        await _cartPage.AddSingleProductToCart();
        await _cartPage.GoToCart();
        await _cartPage.Checkout();
        await _cartPage.FillFirstName("First Name");
        await _cartPage.FillSecondName("Second Name");
        await _cartPage.FillPostalCode("Code");
        await _cartPage.FinishOrder();
        var expectMessage = "Checkout: Complete!";

        await Expect(_cartPage.GetTitle()).ToContainTextAsync(expectMessage);
    }
}