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
    private Dictionary<string, object> _data = new Dictionary<string, object>() { { "id", "" } };

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
    public async Task SuccesfullCreate()
    {
        var createBookingRqst = new CreateRequest(_baseRequest);
        var response = await createBookingRqst.Send(RequestsConstansts.booking);
        var jsonData = await response.TextAsync();
        var receivedJson = JsonDocument.Parse(jsonData);

        await Expect(response).ToBeOKAsync();

        Assert.IsTrue(receivedJson.RootElement.TryGetProperty("bookingid", out var bookingId));
    
        var putRqst = new PutRequest(_baseRequest);
        putRqst.ID = bookingId.GetInt32();

        var putResponse = await putRqst.Send(RequestsConstansts.updBooking);
        var jsonDataPut = await putResponse.TextAsync();
        var receivedJsonPut = JsonDocument.Parse(jsonDataPut);

        await Expect(putResponse).ToBeOKAsync();

        var json = JObject.Parse(jsonDataPut);
        Assert.IsTrue(json.IsValid(Schemas.patch));

        var receivedJsonString = jsonDataPut.ToString();
        var sendedJsonString = JsonSerializer.Serialize(RequestsConstansts.updBooking);
        Assert.AreEqual(receivedJsonString, sendedJsonString);

        _data["id"] = bookingId.GetInt32();
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

