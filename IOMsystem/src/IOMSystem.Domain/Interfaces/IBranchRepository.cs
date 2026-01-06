using IOMSystem.Domain.Entities;

namespace IOMSystem.Domain.Interfaces;

public interface IBranchRepository
{
    Task<List<Branch>> GetAllAsync();
    Task<Branch?> GetByCodeAsync(string code);
    Task AddAsync(Branch branch);
    Task UpdateAsync(Branch branch);
    Task DeleteAsync(string code);
}
