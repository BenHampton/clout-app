using clout_api.Utilities.Base;

namespace clout_api.Utilities;

public class HttpContextUtilities : HttpContextUtilitiesBase
{
    public HttpContextUtilities(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }
}
