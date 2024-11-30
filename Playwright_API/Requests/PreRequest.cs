using Microsoft.Playwright;
using System.Text.Json;

namespace PlaywrightTests;

public static class PreRequest
{
    public static async Task Auth(IAPIRequestContext request){
        var authReq = new AuthRequest(request);

        var data = new Dictionary<string, object>(){
            {"username", RequestsConstansts.username},
            {"password", RequestsConstansts.password}
        };

        var response = await authReq.Send(data);
        var jsonData = await response.TextAsync();
        var jsonDocument = JsonDocument.Parse(jsonData);
        jsonDocument.RootElement.TryGetProperty("token", out var token);

        Headers.SetToken(token.GetString());
    }

    public static async Task CreateBooking(IAPIRequestContext request){
        var createBookingRqst = new CreateRequest(request);
        var response = await createBookingRqst.Send(RequestsConstansts.booking);
        var jsonData = await response.TextAsync();
        var receivedJson = JsonDocument.Parse(jsonData);

        receivedJson.RootElement.TryGetProperty("bookingid", out var bookingId);
        RequestsConstansts.bookingID = bookingId.GetInt32();
    }
}

