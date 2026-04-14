using Crud_App_dotNetCore.Interfaces.IServices;
using Microsoft.AspNetCore.Http;

public class TenantProvider : ITenantProvider
{
    private readonly IHttpContextAccessor _http;    

    public TenantProvider(IHttpContextAccessor http)
    {
        _http = http;
    }

    public int GetTenantId()
    {
        var claim = _http.HttpContext?.User?.FindFirst("appUserID");
        return claim != null ? int.Parse(claim.Value) : 0;
    }
}