using PhoneApp.Application.DTOs.Phone;
using PhoneApp.Application.DTOs.User;
using PhoneApp.Domain.Entities;

namespace PhoneApp.Application.Mapping;

public static class UserMappingExtensions
{
    public static UserDto ToDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth
        };
    }

    public static User ToEntity(this CreateUserDto dto, string passwordHash)
    {
        return new User
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = passwordHash,
            DateOfBirth = dto.DateOfBirth
        };
    }

    public static void UpdateFromDto(this User user, UpdateUserDto dto)
    {
        user.Name = dto.Name;
        user.Email = dto.Email;
        user.DateOfBirth = dto.DateOfBirth;
    }
}