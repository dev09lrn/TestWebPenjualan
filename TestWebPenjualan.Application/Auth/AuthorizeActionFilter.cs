using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TestWebPenjualan.Application.Interfaces;

namespace TestWebPenjualan.Application.Auth;

public class AuthorizeActionFilter : IActionFilter
{
    private readonly ILoginHelper _loginHelper;

    public AuthorizeActionFilter(ILoginHelper loginHelper)
    {
        _loginHelper = loginHelper;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        //throw new NotImplementedException();
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var controllerName = ((ControllerBase)context.Controller).ControllerContext.ActionDescriptor.ControllerName;
        var actionName = ((ControllerBase)context.Controller).ControllerContext.ActionDescriptor.ActionName;

        var isLoginAction = (controllerName.ToLower() == "login" && actionName.ToLower() == "index");


        if (!IsLoggedIn(context) && !isLoginAction)
        {
            RedirectToLoginPage(context);
        }

        if(IsLoggedIn(context) && isLoginAction)
        {
            RedirectToHomePage(context);
        }
    }

    private bool IsLoggedIn(ActionExecutingContext context)
    {
        var username = _loginHelper.GetLoginUsername();

        if (!string.IsNullOrEmpty(username))
        {
            return true;
        }
        return false;
    }

    private void RedirectToLoginPage(ActionExecutingContext context)
    {
        context.Result = new RedirectToRouteResult(new RouteValueDictionary {
                {"Controller", "Login" },
                {"Action", "Index" }
            });
    }

    private void RedirectToHomePage(ActionExecutingContext context)
    {
        context.Result = new RedirectToRouteResult(new RouteValueDictionary {
                {"Controller", "Home" },
                {"Action", "Index" }
            });
    }
}
