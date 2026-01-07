using IOMSystem.Application.Interfaces;
using IOMSystem.Contract.DTOs;
using IOMSystem.Domain.Entities;
using IOMSystem.Domain.Interfaces;

namespace IOMSystem.Application.Services;

public class BranchService : IBranchService
{
    private readonly IBranchRepository _branchRepository;

    public BranchService(IBranchRepository branchRepository)
    {
        _branchRepository = branchRepository;
    }

    public async Task<List<BranchResponseDto>> GetAllBranchesAsync()
    {
        var branches = await _branchRepository.GetAllAsync();
        return branches.Select(b => new BranchResponseDto
        {
            BranchCode = b.BranchCode,
            BranchName = b.BranchName,
            Address = b.Address,
            City = b.City,
            State = b.State,
            PhoneNumber = b.PhoneNumber,
            IsActive = b.IsActive
        }).ToList();
    }

    public async Task<BranchResponseDto?> GetBranchByIdAsync(string code)
    {
        var branch = await _branchRepository.GetByCodeAsync(code);
        if (branch == null) return null;

        return new BranchResponseDto
        {
            BranchCode = branch.BranchCode,
            BranchName = branch.BranchName,
            Address = branch.Address,
            City = branch.City,
            State = branch.State,
            PhoneNumber = branch.PhoneNumber,
            IsActive = branch.IsActive
        };
    }

    public async Task<bool> CreateBranchAsync(CreateBranchDto branchDto)
    {
        // Check local duplicate since PK is user-defined
        if (await _branchRepository.GetByCodeAsync(branchDto.BranchCode) != null) return false;

        var branch = new Branch
        {
            BranchName = branchDto.BranchName,
            BranchCode = branchDto.BranchCode,
            Address = branchDto.Address,
            City = branchDto.City,
            State = branchDto.State,
            PhoneNumber = branchDto.PhoneNumber,
            IsActive = branchDto.IsActive,
            CreatedDate = DateTime.Now
        };

        await _branchRepository.AddAsync(branch);
        return true;
    }

    public async Task<bool> UpdateBranchAsync(string code, UpdateBranchDto branchDto)
    {
        var branch = await _branchRepository.GetByCodeAsync(code);
        if (branch == null) return false;

        branch.BranchName = branchDto.BranchName;
        // BranchCode is PK and immutable in this update
        branch.Address = branchDto.Address;
        branch.City = branchDto.City;
        branch.State = branchDto.State;
        branch.PhoneNumber = branchDto.PhoneNumber;
        branch.IsActive = branchDto.IsActive;

        await _branchRepository.UpdateAsync(branch);
        return true;
    }

    public async Task<bool> UpdateBranchStatusAsync(string code, bool isActive)
    {
        var branch = await _branchRepository.GetByCodeAsync(code);
        if (branch == null) return false;

        branch.IsActive = isActive;
        await _branchRepository.UpdateAsync(branch);
        return true;
    }

    public async Task<bool> DeleteBranchAsync(string code)
    {
        await _branchRepository.DeleteAsync(code);
        return true;
    }
}
