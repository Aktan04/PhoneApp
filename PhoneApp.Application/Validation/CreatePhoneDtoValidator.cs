using FluentValidation;
using PhoneApp.Application.DTOs.Phone;

namespace PhoneApp.Application.Validation;

public class CreatePhoneDtoValidator : AbstractValidator<CreatePhoneDto>
{
    public CreatePhoneDtoValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches(@"^\+?\d{10,15}$")
            .WithMessage("Некорректный номер телефона");
    }
}