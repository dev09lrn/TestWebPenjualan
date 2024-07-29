using Microsoft.AspNetCore.Mvc.Filters;

namespace TestWebPenjualan.API.Auth;

public class AuthorizeActionFilter : IActionFilter, IAuthorizationFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        //throw new NotImplementedException();
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
         
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        throw new NotImplementedException();
    }
}
