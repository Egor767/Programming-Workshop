using System.ComponentModel.DataAnnotations;
using Microsoft.Playwright;
using PlaywrightTests;
using static PlaywrightTests.Locators;

namespace PlaywrightTests;

public class Page{
    protected readonly IPage _page;
    private string _url = "https://www.saucedemo.com/";
    public Page(IPage page)
    {
        _page = page;
    }
    public async Task GotoAsync()
    {
        await _page.GotoAsync(_url);
    }
    public async Task Authorization()
    {
        await _page.Locator(Username).FillAsync("standard_user");
        await _page.Locator(Password).FillAsync("secret_sauce");
        await _page.Locator(LoginButton).ClickAsync();
    }
}

