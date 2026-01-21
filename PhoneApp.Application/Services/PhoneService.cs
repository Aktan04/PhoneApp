using PhoneApp.Application.DTOs.Phone;
using PhoneApp.Application.Interfaces;
using PhoneApp.Application.Mapping;
using PhoneApp.Domain.Entities;
using PhoneApp.Domain.Exceptions;
using PhoneApp.Domain.Interfaces;

namespace PhoneApp.Application.Services;

public class PhoneService : IPhoneService
{
    private readonly IPhoneRepository _phoneRepository;
    private readonly IUserRepository _userRepository;

    public PhoneService(IPhoneRepository phoneRepository, IUserRepository userRepository)
    {
        _phoneRepository = phoneRepository;
        _userRepository = userRepository;
    }

    public async Task<Phone> GetEntityByIdAsync(int id)
    {
        var phone = await _phoneRepository.GetByIdAsync(id);

        if (phone == null)
            throw new NotFoundException("Phone", id);

        return phone;
    }
    
    public async Task<IEnumerable<PhoneDto>> GetByUserIdAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new NotFoundException("User", userId);

        var phones = await _phoneRepository.GetByUserIdAsync(userId);
        return phones.Select(p => p.ToDto());
    }
    
    public async Task<PhoneDto> GetByIdAsync(int id)
    {
        var phone = await _phoneRepository.GetByIdAsync(id);
        if (phone == null)
            throw new NotFoundException("Phone", id);

        return phone.ToDto();
    }

    public async Task<PhoneDto> CreateAsync(int userId, CreatePhoneDto dto)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new NotFoundException("User", userId);

        if (await _phoneRepository.ExistsByNumberAsync(dto.PhoneNumber))
            throw new ValidationException("Телефон с таким номером уже существует");

        var phone = new Phone
        {
            PhoneNumber = dto.PhoneNumber,
            UserId = userId
        };

        await _phoneRepository.CreateAsync(phone);
        await _phoneRepository.SaveChangesAsync();

        return phone.ToDto();
    }

    public async Task<PhoneDto> UpdateAsync(int id, UpdatePhoneDto dto)
    {
        var phone = await _phoneRepository.GetByIdAsync(id);
        if (phone == null)
            throw new NotFoundException("Phone", id);

        if (await _phoneRepository.ExistsByNumberAsync(dto.PhoneNumber, excludePhoneId: id))
            throw new ValidationException("Телефон с таким номером уже существует");

        phone.PhoneNumber = dto.PhoneNumber;

        await _phoneRepository.UpdateAsync(phone);
        await _phoneRepository.SaveChangesAsync();

        return phone.ToDto();
    }

    public async Task DeleteAsync(int id)
    {
        var phone = await _phoneRepository.GetByIdAsync(id);
        if (phone == null)
            throw new NotFoundException("Phone", id);

        await _phoneRepository.DeleteAsync(phone);
        await _phoneRepository.SaveChangesAsync();
    }
}