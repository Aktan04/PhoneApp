using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using PhoneApp.Domain.Exceptions;

namespace PhoneApp.WebApi.Extensions;

public static class HttpContextExtensions
{
    public static int GetUserId(this HttpContext context)
    {
        var userId = context.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (string.IsNullOrEmpty(userId))
        {
            throw new UnauthorizedAccessException("User claim not found");
        }
        return int.Parse(userId);
    }

}