using FluentValidation;
using PhoneApp.Application.DTOs.User;

namespace PhoneApp.Application.Validation;

public class UpdateUserDtoValidator: AbstractValidator<UpdateUserDto>
{
    public UpdateUserDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty().EmailAddress();

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateOnly.FromDateTime(DateTime.UtcNow));
    }
}