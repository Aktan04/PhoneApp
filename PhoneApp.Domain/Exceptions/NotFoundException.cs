namespace PhoneApp.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
    
    public NotFoundException(string entityName, object key) 
        : base($"{entityName} с ID '{key}' не найден") { }
}