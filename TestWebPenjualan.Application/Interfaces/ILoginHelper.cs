using System.Security.Claims;
using TestWebPenjualan.Domain.Dtos.Login;

namespace TestWebPenjualan.Application.Interfaces;

public interface ILoginHelper
{
    Task<bool> ClaimsIdentitySignInAsync(LoginRequestDto loginRequest, LoginResponseDto loginResponse);
    string GetLoginUsername();
    string GetLoginToken();
}
