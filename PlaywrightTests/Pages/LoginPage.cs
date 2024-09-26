using Microsoft.Playwright;
using System.Threading.Tasks;
using static PlaywrightTests.Locators;

namespace PlaywrightTests;

public class LoginPage: Page{
    public LoginPage(IPage page): base(page) {}
    public async Task FillUsername(string username)
    {
        await _page.Locator(Username).FillAsync(username);
    }
    
    public async Task FillPassword(string password)
    {
        await _page.Locator(Password).FillAsync(password);
    }

    public async Task Login()
    {
        await _page.Locator(LoginButton).ClickAsync();
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

