using System.ComponentModel.DataAnnotations;
using Microsoft.Playwright;
using PlaywrightTests;

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
}

