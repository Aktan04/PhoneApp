using System.ComponentModel.DataAnnotations;

namespace PhoneApp.Application.DTOs.Phone;

public class SavePhoneDto
{
    public int? Id { get; set; }
    [Required(ErrorMessage = "Номер телефона обязателен")]
    [Phone(ErrorMessage = "Некорректный формат номера телефона")]
    [StringLength(20, ErrorMessage = "Номер не должен превышать 20 символов")]
    public string PhoneNumber { get; set; } = string.Empty;
    [Required(ErrorMessage = "UserId обязателен")]
    [Range(1, int.MaxValue, ErrorMessage = "Некорректный UserId")]
    public int UserId { get; set; }
}