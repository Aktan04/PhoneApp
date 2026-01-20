using PhoneApp.Domain.Entities;

namespace PhoneApp.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id, bool includePhones = false);
    Task<User?> GetByEmailAsync(string email);
    Task<IEnumerable<User>> GetAllAsync(bool includePhones = false);
    Task<User> CreateAsync(User user);
    Task<User> UpdateAsync(User user);
    Task DeleteAsync(int id);
    Task<bool> ExistsByEmailAsync(string email, int? excludeUserId = null);
    Task<int> SaveChangesAsync();
}