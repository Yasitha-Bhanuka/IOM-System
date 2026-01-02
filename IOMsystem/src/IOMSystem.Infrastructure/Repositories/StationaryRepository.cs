using Microsoft.EntityFrameworkCore;
using IOMSystem.Domain.Entities;
using IOMSystem.Domain.Interfaces;
using IOMSystem.Infrastructure.Data;

namespace IOMSystem.Infrastructure.Repositories;

public class StationaryRepository : IStationaryRepository
{
    private readonly ApplicationDbContext _context;

    public StationaryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Stationary> GetByCodeAsync(string locationCode)
    {
        return await _context.Stationaries
            .FirstOrDefaultAsync(s => s.LocationCode == locationCode);
    }

    public async Task<bool> IsActiveAsync(string locationCode)
    {
        var stationary = await GetByCodeAsync(locationCode);
        return stationary != null && stationary.IsActive;
    }
}
