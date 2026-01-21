namespace PhoneApp.BlazorApp.Models;

public class ApiError
{
    public string? Type { get; set; }
    public string? Message { get; set; }
    public Dictionary<string, string[]>? Errors { get; set; }
}