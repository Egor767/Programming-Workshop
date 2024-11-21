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
    private Dictionary<string, object> _dtd = new Dictionary<string, object>() { { "id", "" } };

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
        //create and send request
        var createBookingRqst = new CreateRequest(_baseRequest);
        var response = await createBookingRqst.Send(RequestsConstansts.booking);
        var jsonData = await response.TextAsync();
        var receivedJson = JsonDocument.Parse(jsonData);

        //check status code
        await Expect(response).ToBeOKAsync();

        //check content
        Assert.IsTrue(receivedJson.RootElement.TryGetProperty("bookingid", out var bookingId));
    
        //create pu request
        var putRqst = new PutRequest(_baseRequest);
        putRqst.ID = bookingId.GetInt32();

        //get response
        var putResponse = await putRqst.Send(RequestsConstansts.updBooking);
        var jsonDataPut = await putResponse.TextAsync();
        var receivedJsonPut = JsonDocument.Parse(jsonDataPut);

        //check status code
        await Expect(putResponse).ToBeOKAsync();

        //schema validating
        var json = JObject.Parse(jsonDataPut);
        Assert.IsTrue(json.IsValid(Schemas.patch));

        //compare
        var receivedJsonString = jsonDataPut.ToString();
        var sendedJsonString = JsonSerializer.Serialize(RequestsConstansts.updBooking);
        Assert.AreEqual(receivedJsonString, sendedJsonString);

        //remember id of created entity 
        _dtd["id"] = bookingId;
    }

    [TestCleanup]
    public async Task TearDownAPITesting()
    {
        //delete created entity
        var deleteBookingRqst = new DeleteRequest(_baseRequest);
        var response = await deleteBookingRqst.Send(_dtd);
        await Expect(response).ToBeOKAsync();

        await _baseRequest.DisposeAsync();
    }
}

