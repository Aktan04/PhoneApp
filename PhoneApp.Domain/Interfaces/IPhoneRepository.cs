using PhoneApp.Domain.Entities;

namespace PhoneApp.Domain.Interfaces;

public interface IPhoneRepository
{
    Task<Phone?> GetByIdAsync(int id, bool includeUser = false);
    Task<IEnumerable<Phone>> GetAllAsync(bool includeUser = false);
    Task<IEnumerable<Phone>> GetByUserIdAsync(int userId);
    Task<Phone> CreateAsync(Phone phone);
    Task<Phone> UpdateAsync(Phone phone);
    Task DeleteAsync(int id);
    Task<bool> ExistsByNumberAsync(string phoneNumber, int? excludePhoneId = null);
    Task<int> SaveChangesAsync();
}