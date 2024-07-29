using TestWebPenjualan.Domain.Dtos.Login;
using TestWebPenjualan.Domain.Dtos.Register;

namespace TestWebPenjualan.Infrastructure.Interfaces;

public interface ICustomAuthenticationService
{
    Task<RegisterResponseDto> Register(RegisterRequestDto request);
    Task<LoginResponseDto> Login(LoginRequestDto request);
}
