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

    [TestInitialize]
    public async Task SetUpAPITesting()
    {
        await CreateAPIRequestContext();
        await PreRequest.Auth(_baseRequest);
        await PreRequest.CreateBooking(_baseRequest);
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
        var putRqst = new PutRequest(_baseRequest);
        putRqst.ID = RequestsConstansts.bookingID;

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
        var response = await deleteBookingRqst.Send(new Dictionary<string, object>() {{"id", RequestsConstansts.bookingID}});
        await Expect(response).ToBeOKAsync();

        await _baseRequest.DisposeAsync();
    }
}

