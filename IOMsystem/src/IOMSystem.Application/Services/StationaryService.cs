using IOMSystem.Application.Interfaces;
using IOMSystem.Domain.Entities;
using IOMSystem.Domain.Interfaces;
using IOMSystem.Application.DTOs;

namespace IOMSystem.Application.Services;

public class StationaryService : IStationaryService
{
    private readonly IStationaryRepository _repository;

    public StationaryService(IStationaryRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<StationaryDto>> GetAllStationariesAsync()
    {
        var stationaries = await _repository.GetAllAsync();
        return stationaries.Select(s => new StationaryDto
        {
            LocationCode = s.LocationCode,
            BranchCode = s.BranchCode,
            Description = s.Description,
            IsActive = s.IsActive,
            CreatedDate = s.CreatedDate
        }).ToList();
    }

    public async Task<StationaryDto?> GetStationaryByCodeAsync(string code)
    {
        var s = await _repository.GetByCodeAsync(code);
        if (s == null) return null;
        return new StationaryDto
        {
            LocationCode = s.LocationCode,
            BranchCode = s.BranchCode,
            Description = s.Description,
            IsActive = s.IsActive,
            CreatedDate = s.CreatedDate
        };
    }

    public async Task<bool> CreateStationaryAsync(StationaryDto dto)
    {
        var exists = await _repository.GetByCodeAsync(dto.LocationCode);
        if (exists != null) return false;

        var stationary = new Stationary
        {
            LocationCode = dto.LocationCode,
            BranchCode = dto.BranchCode,
            Description = dto.Description,
            IsActive = dto.IsActive,
            CreatedDate = DateTime.Now
        };
        await _repository.AddAsync(stationary);
        return true;
    }

    public async Task<bool> UpdateStationaryAsync(string code, StationaryDto dto)
    {
        var stationary = await _repository.GetByCodeAsync(code);
        if (stationary == null) return false;

        stationary.Description = dto.Description;
        stationary.BranchCode = dto.BranchCode;
        stationary.IsActive = dto.IsActive;
        // Don't update LocationCode as it is PK usually, or restricted.

        await _repository.UpdateAsync(stationary);
        return true;
    }

    public async Task<bool> DeleteStationaryAsync(string code)
    {
        var stationary = await _repository.GetByCodeAsync(code);
        if (stationary == null) return false;

        stationary.IsActive = false;
        await _repository.UpdateAsync(stationary);
        return true;
    }
}
