using FluentValidation;
using TestWebPenjualan.Domain.Dtos.Login;

namespace TestWebPenjualan.Domain.Validators;

public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
{
    public LoginRequestDtoValidator()
    {
        RuleFor(dto => dto.Username)
            .NotEmpty().WithMessage("Username is required.");

        RuleFor(dto => dto.Password)
           .NotEmpty().WithMessage("Password is required.");
    }
}
