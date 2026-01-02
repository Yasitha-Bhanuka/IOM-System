using IOMSystem.Application.DTOs;

namespace IOMSystem.Application.Interfaces;

public interface IUserService
{
    Task<UserDto?> LoginAsync(LoginDto loginDto);
    Task<bool> RegisterUserAsync(RegisterUserDto registerDto);
    Task<UserDto?> GetUserByEmailAsync(string email);
    Task<List<UserDto>> GetAllUsersAsync(); // Admin only
}
