using Microsoft.Playwright;

namespace PlaywrightTests;

public class AuthRequest : Request
{
    public AuthRequest(IAPIRequestContext request) : base(request) {}
    public override Task<IAPIResponse> Send(IDictionary<string, object> data)
    {
        return _request.PostAsync(
            "/auth", new()
            {
                DataObject = data
            }
        );
    }
}

