using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using TestWebPenjualan.Application.Interfaces;
using TestWebPenjualan.Domain.Dtos.Login;

namespace TestWebPenjualan.Application.Helpers;

public class LoginHelper : ILoginHelper
{
    private IHttpContextAccessor _contextAccessor;

    public LoginHelper(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public async Task<bool> ClaimsIdentitySignInAsync(LoginRequestDto loginRequest, LoginResponseDto loginResponse)
    {
        var authClaims = new List<Claim>
        {
                            new("UserName", loginRequest.Username),
                            new("Token", loginResponse.Token??""),
                        };

        var claimsIdentity = new ClaimsIdentity(
            authClaims,
            CookieAuthenticationDefaults.AuthenticationScheme);

        if (_contextAccessor.HttpContext != null)
        {
            await _contextAccessor.HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));

            return true;
        }

        return false;
    }

    public string GetLoginToken()
    {
        var token = _contextAccessor.HttpContext != null
                    ? _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Token")?.Value
                    : "";
        return token ?? "";
    }

    public string GetLoginUsername()
    {
        var username = _contextAccessor.HttpContext != null
                        ? _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserName")?.Value
                        : "";
        return username ?? "";
    }
}
