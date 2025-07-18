using System.Security.Claims;

namespace clout_api.Utilities.Base;

public abstract class HttpContextUtilitiesBase
{
    private readonly HttpContext _httpContext;

    [Obsolete("For testing use only")]
    public HttpContextUtilitiesBase() { }

    public HttpContextUtilitiesBase(IHttpContextAccessor httpContextAccessor)
    {
        _httpContext = httpContextAccessor.HttpContext!;
    }

    public virtual string GetUsernameFromHttpContext()
    {
        if (_httpContext is null || _httpContext.User is null)
        {
            return "System";
        }
        var username = _httpContext.User.FindFirstValue(ClaimTypes.Name);

        return username ?? "System";
    }
}
