using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using System.Text.Json;

namespace PlaywrightTests;

public static class Headers{
    public static Dictionary<string, string> headers = new(){
        {"Content-Type", "application/json"},
        {"Cookie", $""}
    };

    public static void SetToken(string token) {
        headers["Cookie"] = $"token={token}";
    }

    public static Dictionary<string, string> GetHeaders(string jsonData) {

        var jsonDocument = JsonDocument.Parse(jsonData);
        jsonDocument.RootElement.TryGetProperty("token", out var token);
        SetToken(token.GetString());

        return headers;
    }
}   

