using IOMSystem.Domain.Entities;

namespace IOMSystem.Domain.Interfaces;

public interface IRegistrationRequestRepository
{
    Task<List<UserRegistrationRequest>> GetPendingAsync();
    Task<List<UserRegistrationRequest>> GetAllAsync();
    Task<UserRegistrationRequest?> GetByIdAsync(int id);
    Task<bool> PendingExistsAsync(string email);
    Task AddAsync(UserRegistrationRequest request);
    Task UpdateAsync(UserRegistrationRequest request);
    Task DeleteAsync(UserRegistrationRequest request);
}
