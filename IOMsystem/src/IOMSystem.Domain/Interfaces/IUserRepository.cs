using IOMSystem.Domain.Entities;

namespace IOMSystem.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(int id);
    Task AddAsync(User user);
    Task<bool> EmailExistsAsync(string email);
    Task AddRoleAsync(Role role);
    Task<Role?> GetRoleByNameAsync(string roleName);
}
