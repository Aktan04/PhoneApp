using System.Security.Claims;
using PhoneApp.Domain.Entities;

namespace PhoneApp.Application.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(User user);
}
