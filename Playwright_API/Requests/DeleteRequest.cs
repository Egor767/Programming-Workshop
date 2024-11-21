using Microsoft.Playwright;

namespace PlaywrightTests;

public class DeleteRequest : Request
{
    public DeleteRequest(IAPIRequestContext request) : base(request) {}
    public override Task<IAPIResponse> Send(IDictionary<string, object> data)
    {
        var id = data["id"];
        return _request.DeleteAsync($"/booking/{id}");
    }

}

