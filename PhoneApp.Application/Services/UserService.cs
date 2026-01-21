using PhoneApp.Application.DTOs.User;
using PhoneApp.Application.Interfaces;
using PhoneApp.Application.Mapping;
using PhoneApp.Domain.Entities;
using PhoneApp.Domain.Exceptions;
using PhoneApp.Domain.Interfaces;

namespace PhoneApp.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            throw new NotFoundException("User", id);

        return user.ToDto();
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(u => u.ToDto());
    }

    public async Task<UserDto> CreateAsync(CreateUserDto dto)
    {
        if (await _userRepository.ExistsByEmailAsync(dto.Email))
            throw new ValidationException("Пользователь с таким email уже существует");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        var user = dto.ToEntity(passwordHash);

        await _userRepository.CreateAsync(user);
        await _userRepository.SaveChangesAsync();

        return user.ToDto();
    }

    public async Task<UserDto> UpdateAsync(int id, UpdateUserDto dto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            throw new NotFoundException("User", id);

        if (await _userRepository.ExistsByEmailAsync(dto.Email, excludeUserId: id))
            throw new ValidationException("Пользователь с таким email уже существует");

        user.UpdateFromDto(dto);

        await _userRepository.UpdateAsync(user);
        await _userRepository.SaveChangesAsync();

        return user.ToDto();
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            throw new NotFoundException("User", id);

        await _userRepository.DeleteAsync(user);
        await _userRepository.SaveChangesAsync();
    }
}