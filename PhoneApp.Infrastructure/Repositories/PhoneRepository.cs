using Microsoft.EntityFrameworkCore;
using PhoneApp.Domain.Entities;
using PhoneApp.Domain.Interfaces;
using PhoneApp.Infrastructure.Data;

namespace PhoneApp.Infrastructure.Repositories;

public class PhoneRepository : IPhoneRepository
{
    private readonly ApplicationDbContext _context;

    public PhoneRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Phone?> GetByIdAsync(int id)
        => await _context.Phones.FirstOrDefaultAsync(p => p.Id == id);

    public async Task<IEnumerable<Phone>> GetByUserIdAsync(int userId)
        => await _context.Phones
            .Where(p => p.UserId == userId)
            .ToListAsync();

    public async Task<bool> ExistsByNumberAsync(string phoneNumber, int? excludePhoneId = null)
    {
        var query = _context.Phones.Where(p => p.PhoneNumber == phoneNumber);

        if (excludePhoneId.HasValue)
            query = query.Where(p => p.Id != excludePhoneId.Value);

        return await query.AnyAsync();
    }

    public async Task CreateAsync(Phone phone)
    {
        await _context.Phones.AddAsync(phone);
    }

    public Task UpdateAsync(Phone phone)
    {
        _context.Phones.Update(phone);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Phone phone)
    {
        _context.Phones.Remove(phone);
        return Task.CompletedTask;
    }

    public async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();
}