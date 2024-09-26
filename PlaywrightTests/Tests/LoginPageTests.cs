using Microsoft.Playwright.MSTest;

namespace PlaywrightTests;

[TestClass]
public class LoginPageTests: PageTest
{
    private LoginPage _loginPage;

    [TestInitialize]
    public void NewPage()
    {
        _loginPage = new LoginPage(Page);
    }

    [TestMethod]
    public async Task FillWrongPassword()
    {
        await _loginPage.GotoAsync();
        await _loginPage.FillUsername("standard_user");
        await _loginPage.FillPassword("WrongPassword");
        await _loginPage.Login();
        string expectMessage = "Epic sadface: Username and password do not match any user in this service";

        await Expect(_loginPage.GetErrorMessage()).ToContainTextAsync(expectMessage);
    }

    [TestMethod]
    public async Task FillRightPasswordLockedOutUser()
    {
        await _loginPage.GotoAsync();
        await _loginPage.FillUsername("locked_out_user");
        await _loginPage.FillPassword("secret_sauce");
        await _loginPage.Login();
        string expectMessage = "Epic sadface: Sorry, this user has been locked out.";

        await Expect(_loginPage.GetErrorMessage()).ToContainTextAsync(expectMessage);
    } 

    [TestMethod]
    public async Task LoginWithEmptyFields()
    {
        await _loginPage.GotoAsync();
        await _loginPage.Login();
        string expectMessage = "Epic sadface: Username is required";

        await Expect(_loginPage.GetErrorMessage()).ToContainTextAsync(expectMessage);
    }

    [TestMethod]
    public async Task SuccessfulLogin()
    {
        await _loginPage.GotoAsync();
        await _loginPage.FillUsername("standard_user");
        await _loginPage.FillPassword("secret_sauce");
        await _loginPage.Login();
        string expectMessage = "Products";

        await Expect(_loginPage.GetTitle()).ToContainTextAsync(expectMessage);
    } 
}

