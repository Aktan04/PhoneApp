namespace PhoneApp.Domain.Exceptions;

public class ValidationException : Exception
{
    public Dictionary<string, string[]> Errors { get; }
    
    public ValidationException(string message) : base(message)
    {
        Errors = new Dictionary<string, string[]>();
    }
    
    public ValidationException(Dictionary<string, string[]> errors) 
        : base("Ошибки валидации")
    {
        Errors = errors;
    }
}