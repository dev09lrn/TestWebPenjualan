using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestWebPenjualan.Domain.Dtos.Login;
using TestWebPenjualan.Domain.Dtos.Register;
using TestWebPenjualan.Domain.Entities;
using TestWebPenjualan.Domain.Helpers;
using TestWebPenjualan.Infrastructure.Interfaces;

namespace TestWebPenjualan.Infrastructure.Services;

public class CustomAuthenticationService : ICustomAuthenticationService
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public CustomAuthenticationService(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<RegisterResponseDto> Register(RegisterRequestDto request)
    {
        var userByEmail = await _userManager.FindByEmailAsync(request.Email);
        var userByUsername = await _userManager.FindByNameAsync(request.Username);
        if (userByEmail is not null || userByUsername is not null)
        {
            return new RegisterResponseDto
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Success = false,
                Message = UserRegisterMessageHelper.GetInfoRegisterFailUserAlreadyExists()
            };
        }

        User user = new()
        {
            Email = request.Email,
            UserName = request.Username,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return new RegisterResponseDto
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Success = false,
                Message = GetErrorsText(result.Errors)
            };
        }

        return new RegisterResponseDto
        {
            StatusCode = StatusCodes.Status200OK,
            Success = true,
            Message = UserRegisterMessageHelper.GetInfoRegisterSuccess()
        };
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto request)
    {
        var message = "";

        var user = await _userManager.FindByNameAsync(request.Username);

        if (user is null)
        {
            user = await _userManager.FindByEmailAsync(request.Username);
        }

        if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            message = LoginMessageHelper.GetInfoInvalidUsernameOrPassword();

            return new LoginResponseDto { 
                StatusCode = StatusCodes.Status400BadRequest, 
                Success = false, 
                Message = message, 
                Token = "" 
            };
        }

        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var token = GetToken(authClaims);
        message = LoginMessageHelper.GetInfoLoginSuccess();

        var loginToken = new JwtSecurityTokenHandler().WriteToken(token);

        return new LoginResponseDto { 
            StatusCode = StatusCodes.Status200OK, 
            Success = true, 
            Message = message, 
            Token = loginToken 
        };
    }

    private JwtSecurityToken GetToken(IEnumerable<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

        return token;
    }

    private string GetErrorsText(IEnumerable<IdentityError> errors)
    {
        return string.Join(", ", errors.Select(error => error.Description).ToArray());
    }
}
