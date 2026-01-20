namespace PhoneApp.Application.DTOs.Phone;

public class PhoneDto
{
    public int Id { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
}
