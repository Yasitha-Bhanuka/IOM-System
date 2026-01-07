using IOMSystem.Contract.DTOs;

namespace IOMSystem.Application.Interfaces;

public interface IBranchService
{
    Task<List<BranchResponseDto>> GetAllBranchesAsync();
    Task<BranchResponseDto?> GetBranchByIdAsync(string code);
    Task<bool> CreateBranchAsync(CreateBranchDto branchDto);
    Task<bool> UpdateBranchAsync(string code, UpdateBranchDto branchDto);
    Task<bool> UpdateBranchStatusAsync(string code, bool isActive);
    Task<bool> DeleteBranchAsync(string code);
}
