using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace PlaywrightTests;

[TestClass]
public class PutTest : PlaywrightTest
{
    private IAPIRequestContext _baseRequest = null!;
    private Dictionary<string, object> _data = new Dictionary<string, object>() { { "id", 0 } };

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
            ExtraHTTPHeaders = Headers.GetHeaders(await FillToken())
        });
        await CreateBooking();
    }

    public async Task<string> FillToken()
    {
        var authRequest = new AuthRequest(await Playwright.APIRequest.NewContextAsync(new() {
            BaseURL = RequestsConstansts.baseUrl
        }));

        var data = new Dictionary<string, object>(){
            {"username", RequestsConstansts.username},
            {"password", RequestsConstansts.password}
        };

        var response = await authRequest.Send(data);
        
        return await response.TextAsync();
    }

    public async Task CreateBooking()
    {
        var createBookingRqst = new CreateRequest(_baseRequest);
        var response = await createBookingRqst.Send(RequestsConstansts.booking);
        var jsonData = await response.TextAsync();
        var receivedJson = JsonDocument.Parse(jsonData);

        receivedJson.RootElement.TryGetProperty("bookingid", out var bookingId);
        _data["id"] = bookingId.GetInt32();
    }

    [TestMethod]
    public async Task SuccesfullCreate()
    {
        var putRqst = new PutRequest(_baseRequest);
        putRqst.ID = (int)_data["id"];

        var putResponse = await putRqst.Send(RequestsConstansts.updBooking);
        var jsonDataPut = await putResponse.TextAsync();

        await Expect(putResponse).ToBeOKAsync();

        var json = JObject.Parse(jsonDataPut);
        Assert.IsTrue(json.IsValid(Schemas.patch));

        var receivedJsonString = jsonDataPut.ToString();
        var sendedJsonString = JsonSerializer.Serialize(RequestsConstansts.updBooking);
        Assert.AreEqual(receivedJsonString, sendedJsonString);
    }

    [TestCleanup]
    public async Task TearDownAPITesting()
    {
        var deleteBookingRqst = new DeleteRequest(_baseRequest);
        var response = await deleteBookingRqst.Send(_data);
        await Expect(response).ToBeOKAsync();

        await _baseRequest.DisposeAsync();
    }
}

