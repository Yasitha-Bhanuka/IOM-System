using IOMSystem.Application.DTOs;

namespace IOMSystem.Application.Interfaces;

public interface IBranchService
{
    Task<List<BranchDto>> GetAllBranchesAsync();
    Task<BranchDto?> GetBranchByIdAsync(int id);
    Task<bool> CreateBranchAsync(BranchDto branchDto);
    Task<bool> UpdateBranchAsync(int id, BranchDto branchDto);
    Task<bool> DeleteBranchAsync(int id);
}
