using FluentValidation;
using TestWebPenjualan.Domain.Dtos.Register;

namespace TestWebPenjualan.Domain.Validators;

public class RegisterRequestDtoValidator : AbstractValidator<RegisterRequestDto>
{
    public RegisterRequestDtoValidator()
    {
        RuleFor(dto => dto.Username)
            .NotEmpty().WithMessage("Username is required.");

        RuleFor(dto => dto.Email)
            .EmailAddress()
            .NotEmpty()
            .WithMessage("Valid Email is required.");

        RuleFor(dto => dto.Password)
           .NotEmpty().WithMessage("Password is required.");
    }
}
