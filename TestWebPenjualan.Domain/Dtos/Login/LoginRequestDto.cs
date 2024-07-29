using System.ComponentModel.DataAnnotations;

namespace TestWebPenjualan.Domain.Dtos.Login;

public class LoginRequestDto
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
}
