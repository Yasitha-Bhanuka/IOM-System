using Microsoft.EntityFrameworkCore;
using IOMSystem.Domain.Entities;
using IOMSystem.Domain.Interfaces;
using IOMSystem.Infrastructure.Data;

namespace IOMSystem.Infrastructure.Repositories;

public class RegistrationRequestRepository : IRegistrationRequestRepository
{
    private readonly ApplicationDbContext _context;

    public RegistrationRequestRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserRegistrationRequest>> GetPendingAsync()
    {
        return await _context.UserRegistrationRequests
            .Where(r => r.Status == "Pending")
            .OrderByDescending(r => r.RequestDate)
            .ToListAsync();
    }

    public async Task<List<UserRegistrationRequest>> GetAllAsync()
    {
        return await _context.UserRegistrationRequests
            .OrderByDescending(r => r.RequestDate)
            .ToListAsync();
    }

    public async Task<UserRegistrationRequest?> GetByIdAsync(int id)
    {
        return await _context.UserRegistrationRequests.FindAsync(id);
    }

    public async Task<bool> PendingExistsAsync(string email)
    {
        return await _context.UserRegistrationRequests
            .AnyAsync(r => r.UserEmail == email && r.Status == "Pending");
    }

    public async Task AddAsync(UserRegistrationRequest request)
    {
        await _context.UserRegistrationRequests.AddAsync(request);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(UserRegistrationRequest request)
    {
        _context.UserRegistrationRequests.Update(request);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(UserRegistrationRequest request)
    {
        _context.UserRegistrationRequests.Remove(request);
        await _context.SaveChangesAsync();
    }
}
