using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PhoneApp.WebApi.Filters;

public class FluentValidationExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ValidationException fvEx)
        {
            var errors = fvEx.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            throw new PhoneApp.Domain.Exceptions.ValidationException(errors);
        }
    }
}