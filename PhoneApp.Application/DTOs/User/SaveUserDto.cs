using System.ComponentModel.DataAnnotations;
namespace PhoneApp.Application.DTOs.User;

public class SaveUserDto
{
    public int? Id { get; set; }
    [Required(ErrorMessage = "Имя обязательно")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Имя от 2 до 100 символов")]
    public string Name { get; set; } = string.Empty;
    [Required(ErrorMessage = "Email обязателен")]
    [EmailAddress(ErrorMessage = "Некорректный формат email")]
    [StringLength(255)]
    public string Email { get; set; } = string.Empty;
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль от 6 до 100 символов")]
    public string? Password { get; set; }
    [Required(ErrorMessage = "Дата рождения обязательна")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }
    public bool IsActive { get; set; } = true;
}