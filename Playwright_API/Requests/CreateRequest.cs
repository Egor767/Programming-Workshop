using Microsoft.Playwright;

namespace PlaywrightTests;

public class CreateRequest : Request
{
    public CreateRequest(IAPIRequestContext request) : base(request) {}
    public override Task<IAPIResponse> Send(IDictionary<string, object> data)
    {
        return _request.PostAsync(
            "/booking", new()
            {
                DataObject = data
            }
        );
    }
}

