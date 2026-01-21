using PhoneApp.Application.DTOs.Auth;
using PhoneApp.Application.Interfaces;
using PhoneApp.Domain.Entities;
using PhoneApp.Domain.Exceptions;
using PhoneApp.Domain.Interfaces;

namespace PhoneApp.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public AuthService(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<TokenResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);
        if (user == null)
            throw new UnauthorizedException("Неверный email или пароль");

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            throw new UnauthorizedException("Неверный email или пароль");

        var token = _tokenService.GenerateAccessToken(user);

        return new TokenResponseDto
        {
            AccessToken = token
        };
    }

    public async Task<TokenResponseDto> RegisterAsync(RegisterDto dto)
    {
        if (await _userRepository.ExistsByEmailAsync(dto.Email))
            throw new ValidationException("Пользователь с таким email уже существует");

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            DateOfBirth = dto.DateOfBirth,
            Role = UserRole.User
        };

        await _userRepository.CreateAsync(user);
        await _userRepository.SaveChangesAsync();

        var token = _tokenService.GenerateAccessToken(user);

        return new TokenResponseDto
        {
            AccessToken = token
        };
    }
}