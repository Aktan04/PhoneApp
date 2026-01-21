using PhoneApp.Domain.Entities;

namespace PhoneApp.Domain.Interfaces;

public interface IPhoneRepository
{
    Task<Phone?> GetByIdAsync(int id);
    Task<IEnumerable<Phone>> GetByUserIdAsync(int userId);
    Task<bool> ExistsByNumberAsync(string phoneNumber, int? excludePhoneId = null);
    Task CreateAsync(Phone phone);
    Task UpdateAsync(Phone phone);
    Task DeleteAsync(Phone phone);
    Task<int> SaveChangesAsync();
}