namespace PhoneApp.Domain.Entities;

public class Phone
{
    public int Id { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    
    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;
}