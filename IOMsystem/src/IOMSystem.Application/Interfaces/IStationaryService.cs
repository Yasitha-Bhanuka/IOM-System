using IOMSystem.Contracts.DTOs;

namespace IOMSystem.Application.Interfaces;

public interface IStationaryService
{
    Task<List<StationaryDto>> GetAllStationariesAsync();
    Task<StationaryDto?> GetStationaryByCodeAsync(string code);
    Task<bool> CreateStationaryAsync(StationaryDto dto);
    Task<bool> UpdateStationaryAsync(string code, StationaryDto dto);
    Task<bool> DeleteStationaryAsync(string code); // Soft delete
}
