using PhoneApp.Application.DTOs.Phone;
using PhoneApp.Domain.Entities;

namespace PhoneApp.Application.Mapping;

public static class PhoneMappingExtensions
{
    public static PhoneDto ToDto(this Phone phone)
    {
        return new PhoneDto
        {
            Id = phone.Id,
            PhoneNumber = phone.PhoneNumber
        };
    }
    
}