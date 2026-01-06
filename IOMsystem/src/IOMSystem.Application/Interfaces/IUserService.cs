using IOMSystem.Contracts.DTOs;

namespace IOMSystem.Application.Interfaces;

public interface IUserService
{
    Task<UserDto?> LoginAsync(LoginDto loginDto);
    Task<bool> RegisterUserAsync(RegisterUserDto registerDto);
    Task<UserDto?> GetUserByEmailAsync(string email);
    Task<UserDto?> GetUserByIdAsync(int userId);
    Task<List<UserDto>> GetAllUsersAsync();
    Task<bool> UpdateUserAsync(int userId, UserDto userDto);
    Task<bool> ChangePasswordAsync(int userId, string newPassword);
    Task<bool> DeleteUserAsync(int userId); // Soft delete via Update or strict
}
