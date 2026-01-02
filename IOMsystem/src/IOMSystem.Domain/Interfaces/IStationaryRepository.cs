using IOMSystem.Domain.Entities;

namespace IOMSystem.Domain.Interfaces;

public interface IStationaryRepository
{
    Task<Stationary> GetByCodeAsync(string locationCode);
    Task<bool> IsActiveAsync(string locationCode);
}
