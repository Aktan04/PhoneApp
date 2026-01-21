using FluentValidation;
using PhoneApp.Application.DTOs.Phone;

namespace PhoneApp.Application.Validation;

public class UpdatePhoneDtoValidator : AbstractValidator<UpdatePhoneDto>
{
    public UpdatePhoneDtoValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches(@"^\+?\d{10,15}$");
    }
}
