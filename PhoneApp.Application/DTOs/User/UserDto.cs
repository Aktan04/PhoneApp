using PhoneApp.Application.DTOs.Phone;

namespace PhoneApp.Application.DTOs.User;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public bool IsActive { get; set; }
    public List<PhoneDto> Phones { get; set; } = new();
}