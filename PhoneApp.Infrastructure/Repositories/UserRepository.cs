using Microsoft.EntityFrameworkCore;
using PhoneApp.Domain.Entities;
using PhoneApp.Domain.Interfaces;
using PhoneApp.Infrastructure.Data;

namespace PhoneApp.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(int id)
        => await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

    public async Task<User?> GetByEmailAsync(string email)
        => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<IEnumerable<User>> GetAllAsync()
        => await _context.Users.ToListAsync();

    public async Task<bool> ExistsByEmailAsync(string email, int? excludeUserId = null)
    {
        var query = _context.Users.Where(u => u.Email == email);

        if (excludeUserId.HasValue)
            query = query.Where(u => u.Id != excludeUserId.Value);

        return await query.AnyAsync();
    }

    public async Task CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);
        return Task.CompletedTask;
    }

    public async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();
}