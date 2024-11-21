using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;

namespace PlaywrightTests;

public static class Headers{
    public static Dictionary<string, string> headers = new(){
        {"Content-Type", "application/json"},
        {"Cookie", $""}
    };

    public static void SetToken(string token) {
        headers["Cookie"] = $"token={token}";
    }
}   

