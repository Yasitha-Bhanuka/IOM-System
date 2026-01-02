using System.Security.Cryptography;
using System.Text;
using IOMSystem.Application.DTOs;
using IOMSystem.Application.Interfaces;
using IOMSystem.Domain.Entities;
using IOMSystem.Domain.Interfaces;

namespace IOMSystem.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto?> LoginAsync(LoginDto loginDto)
    {
        var user = await _userRepository.GetByEmailAsync(loginDto.Email);
        if (user == null || !user.IsActive) return null;

        if (!VerifyPassword(loginDto.Password, user.PasswordHash, user.PasswordSalt))
            return null;

        return new UserDto
        {
            UserId = user.UserId,
            UserEmail = user.UserEmail,
            FullName = user.FullName ?? "",
            BranchName = user.BranchName,
            RoleName = user.Role?.RoleName ?? "Unknown",
            IsActive = user.IsActive,
            CreatedDate = user.CreatedDate
        };
    }

    public async Task<bool> RegisterUserAsync(RegisterUserDto registerDto)
    {
        if (await _userRepository.EmailExistsAsync(registerDto.Email))
            return false;

        CreatePasswordHash(registerDto.Password, out string passwordHash, out string passwordSalt);

        // Assign default role if exists, else create or error? 
        // For migration: assume role exists or default to "User"
        var role = await _userRepository.GetRoleByNameAsync(registerDto.RoleName)
                   ?? await _userRepository.GetRoleByNameAsync("User");

        if (role == null) return false; // Or throw

        var user = new User
        {
            UserEmail = registerDto.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt, // Storing as string in legacy style if needed, or Base64
            BranchName = registerDto.BranchName,
            RoleId = role.RoleId,
            FullName = registerDto.FullName,
            IsActive = true,
            CreatedDate = DateTime.Now
        };

        await _userRepository.AddAsync(user);
        return true;
    }

    public async Task<UserDto?> GetUserByEmailAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null) return null;

        return new UserDto
        {
            UserId = user.UserId,
            UserEmail = user.UserEmail,
            FullName = user.FullName ?? "",
            BranchName = user.BranchName,
            RoleName = user.Role?.RoleName ?? "Unknown",
            IsActive = user.IsActive,
            CreatedDate = user.CreatedDate
        };
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        // Not implemented in repo yet, but simple enough to add later
        return new List<UserDto>();
    }

    // Helper methods for password hashing (using SHA256 as per likely legacy or improved standard)
    private void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
    {
        using (var hmac = new HMACSHA256())
        {
            passwordSalt = Convert.ToBase64String(hmac.Key);
            passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }

    private bool VerifyPassword(string password, string storedHash, string storedSalt)
    {
        var saltBytes = Convert.FromBase64String(storedSalt);
        var hashBytes = Convert.FromBase64String(storedHash);

        using (var hmac = new HMACSHA256(saltBytes))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != hashBytes[i]) return false;
            }
        }
        return true;
    }
}
