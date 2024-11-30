using Microsoft.Playwright;
using Microsoft.VisualBasic;

namespace PlaywrightTests;

public class PutRequest : Request
{
    public int ID {get; set;}
    public PutRequest(IAPIRequestContext request) : base(request) {}
    public override Task<IAPIResponse> Send(IDictionary<string, object> data)
    {
        return _request.PutAsync(
            $"/booking/{ID}", new()
            {
                DataObject = data
            }
        );
    }
}

