namespace PhoneApp.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public UserRole Role { get; set; } = UserRole.User;

    public ICollection<Phone> Phones { get; set; } = new List<Phone>();
}