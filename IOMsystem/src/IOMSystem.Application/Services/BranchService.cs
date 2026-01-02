using IOMSystem.Application.DTOs;
using IOMSystem.Application.Interfaces;
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

    public async Task<List<BranchDto>> GetAllBranchesAsync()
    {
        var branches = await _branchRepository.GetAllAsync();
        return branches.Select(b => new BranchDto
        {
            BranchId = b.BranchId,
            BranchName = b.BranchName,
            BranchCode = b.BranchCode,
            Address = b.Address,
            City = b.City,
            State = b.State,
            PhoneNumber = b.PhoneNumber,
            IsActive = b.IsActive
        }).ToList();
    }

    public async Task<BranchDto?> GetBranchByIdAsync(int id)
    {
        var branch = await _branchRepository.GetByIdAsync(id);
        if (branch == null) return null;

        return new BranchDto
        {
            BranchId = branch.BranchId,
            BranchName = branch.BranchName,
            BranchCode = branch.BranchCode,
            Address = branch.Address,
            City = branch.City,
            State = branch.State,
            PhoneNumber = branch.PhoneNumber,
            IsActive = branch.IsActive
        };
    }

    public async Task<bool> CreateBranchAsync(BranchDto branchDto)
    {
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

    public async Task<bool> UpdateBranchAsync(int id, BranchDto branchDto)
    {
        var branch = await _branchRepository.GetByIdAsync(id);
        if (branch == null) return false;

        branch.BranchName = branchDto.BranchName;
        branch.BranchCode = branchDto.BranchCode;
        branch.Address = branchDto.Address;
        branch.City = branchDto.City;
        branch.State = branchDto.State;
        branch.PhoneNumber = branchDto.PhoneNumber;
        branch.IsActive = branchDto.IsActive;

        await _branchRepository.UpdateAsync(branch);
        return true;
    }

    public async Task<bool> DeleteBranchAsync(int id)
    {
        await _branchRepository.DeleteAsync(id);
        return true;
    }
}
