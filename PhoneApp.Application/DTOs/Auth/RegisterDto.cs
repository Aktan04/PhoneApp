using System.ComponentModel.DataAnnotations;

namespace PhoneApp.Application.DTOs.Auth;

public class RegisterDto
{
    [Required(ErrorMessage = "Имя обязательно")]
    [StringLength(25, MinimumLength = 6, ErrorMessage = "Имя должно быть от 2 до 25 символов")]
    public string Name { get; set; } = string.Empty;
    [Required(ErrorMessage = "Email обязателен")]
    [EmailAddress(ErrorMessage = "Некорректный формат email")]
    [StringLength(255)]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Пароль обязателен")]
    [MinLength(6, ErrorMessage = "Минимум 6 символов")]
    public string Password { get; set; } = string.Empty;
    [Required(ErrorMessage = "Подтверждение пароля обязательно")]
    [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
    public string ConfirmPassword { get; set; } = string.Empty;
    [Required(ErrorMessage = "Дата рождения обязательна")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }
}