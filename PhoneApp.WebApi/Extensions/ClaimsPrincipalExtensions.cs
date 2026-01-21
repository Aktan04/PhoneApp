using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PhoneApp.WebApi.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static int GetUserId(this ClaimsPrincipal user)
    {
        var id = user.FindFirstValue(JwtRegisteredClaimNames.Sub);

        if (string.IsNullOrEmpty(id))
            throw new UnauthorizedAccessException("UserId claim not found");

        return int.Parse(id);
    }

    public static bool IsAdmin(this ClaimsPrincipal user)
        => user.IsInRole("Admin");
}