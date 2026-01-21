using PhoneApp.Application.DTOs.Phone;
using PhoneApp.Domain.Entities;

namespace PhoneApp.Application.Interfaces;

public interface IPhoneService
{
    Task<Phone> GetEntityByIdAsync(int id);
    Task<IEnumerable<PhoneDto>> GetByUserIdAsync(int userId);
    Task<PhoneDto> GetByIdAsync(int id);
    Task<PhoneDto> CreateAsync(int userId, CreatePhoneDto dto);
    Task<PhoneDto> UpdateAsync(int id, UpdatePhoneDto dto);
    Task DeleteAsync(int id);
}