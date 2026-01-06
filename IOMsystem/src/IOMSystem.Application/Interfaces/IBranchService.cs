using IOMSystem.Application.DTOs;

namespace IOMSystem.Application.Interfaces;

public interface IBranchService
{
    Task<List<BranchResponseDto>> GetAllBranchesAsync();
    Task<BranchResponseDto?> GetBranchByIdAsync(string code);
    Task<bool> CreateBranchAsync(CreateBranchDto branchDto);
    Task<bool> UpdateBranchAsync(string code, UpdateBranchDto branchDto);
    Task<bool> DeleteBranchAsync(string code);
}
