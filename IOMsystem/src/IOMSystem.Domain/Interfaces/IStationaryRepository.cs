using IOMSystem.Domain.Entities;

namespace IOMSystem.Domain.Interfaces;

public interface IStationaryRepository
{
    Task<List<Stationary>> GetAllAsync();
    Task<Stationary?> GetByCodeAsync(string locationCode);
    Task<bool> IsActiveAsync(string locationCode);
    Task AddAsync(Stationary stationary);
    Task UpdateAsync(Stationary stationary);
    // Delete is soft delete via update, but strict delete or logic handling might need repo support if we change approach
}
