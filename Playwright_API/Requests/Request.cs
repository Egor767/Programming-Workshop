using Microsoft.Playwright;

namespace PlaywrightTests;

public class Request
{
    protected IAPIRequestContext _request;
    public Request(IAPIRequestContext request)
    {
        _request = request;
    }

    public virtual Task<IAPIResponse> Send(IDictionary<string, object> data)
    {
        return _request.GetAsync("");
    }
}

