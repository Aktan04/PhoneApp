using PhoneApp.Application.DTOs.Auth;

namespace PhoneApp.Application.Interfaces;

public interface IAuthService
{
    Task<TokenResponseDto> LoginAsync(LoginDto loginDto);
    Task<TokenResponseDto> RegisterAsync(RegisterDto registerDto);
}