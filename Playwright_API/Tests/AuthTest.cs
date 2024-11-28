using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace PlaywrightTests;

[TestClass]
public class AuthTest : PlaywrightTest
{
    private IAPIRequestContext _baseRequest = null!;

    [TestInitialize]
    public async Task SetUpAPITesting()
    {
        await CreateAPIRequestContext();
    }

    private async Task CreateAPIRequestContext()
    {
        _baseRequest = await Playwright.APIRequest.NewContextAsync(new()
        {
            BaseURL = RequestsConstansts.baseUrl,
            ExtraHTTPHeaders = Headers.headers
        });
    }

    [TestMethod]
    public async Task SuccesfullAuth()
    {
        var authRequest = new AuthRequest(_baseRequest);

        var data = new Dictionary<string, object>(){
            {"username", RequestsConstansts.username},
            {"password", RequestsConstansts.password}
        };
        
        var response = await authRequest.Send(data);
        var jsonData = await response.TextAsync();

        await Expect(response).ToBeOKAsync();

        var json = JObject.Parse(jsonData);
        Assert.IsTrue(json.IsValid(Schemas.auth));

        var jsonDocument = JsonDocument.Parse(jsonData);
        Assert.IsTrue(jsonDocument.RootElement.TryGetProperty("token", out var token));
    }

    [TestCleanup]
    public async Task TearDownAPITesting()
    {
        await _baseRequest.DisposeAsync();
    }
}

