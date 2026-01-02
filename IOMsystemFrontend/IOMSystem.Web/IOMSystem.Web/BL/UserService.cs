using System;
using System.Collections.Generic;
using System.Linq;
using InventoryManagementSystem.Helpers;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.BL
{
    public class UserService
    {
        public UserService()
        {
        }

        // Get all users
        public List<User> GetAllUsers()
        {
            var dtos = ApiClient.Instance.Get<List<UserDto>>("users");
            if (dtos == null) return new List<User>();
            return dtos.Select(MapToEntity).ToList();
        }

        // Get active users
        public List<User> GetActiveUsers()
        {
            var all = GetAllUsers();
            return all.Where(u => u.IsActive).ToList();
        }

        // Get user by ID
        public User GetUserById(int userId)
        {
            var dto = ApiClient.Instance.Get<UserDto>($"users/id/{userId}");
            return dto != null ? MapToEntity(dto) : null;
        }

        // Get user by email
        public User GetUserByEmail(string email)
        {
            var dto = ApiClient.Instance.Get<UserDto>($"users/{email}");
            return dto != null ? MapToEntity(dto) : null;
        }

        // Get users by role - Client side filter for now
        public List<User> GetUsersByRole(int roleId)
        {
            // Note: API returns RoleName string, Legacy used RoleId. 
            // We might need to fetch roles or mapping. 
            // Legacy RoleId logic: 1=Admin, 2=Manager, 3=User etc.
            // If we strictly rely on RoleName from API, we might need a helper.
            // Let's assume standard filtering logic or update caller if possible.
            // For now, fetch all and filter if Role object is populated properly.
            var all = GetAllUsers();
            return all.Where(u => u.RoleId == roleId || (u.Role != null && u.Role.RoleId == roleId)).ToList();
        }

        // Get users by branch
        public List<User> GetUsersByBranch(string branchName)
        {
            var all = GetAllUsers();
            if (string.IsNullOrEmpty(branchName)) return all;
            return all.Where(u => u.BranchName.Equals(branchName, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // Update user
        public bool UpdateUser(User user)
        {
            if (user == null) return false;

            var dto = new UserDto
            {
                UserId = user.UserId,
                UserEmail = user.UserEmail, // Usually generic update doesn't change email/ID
                FullName = user.FullName,
                BranchName = user.BranchName,
                RoleName = user.Role?.RoleName ?? "User", // Fallback
                IsActive = user.IsActive,
                CreatedDate = user.CreatedDate
            };

            return ApiClient.Instance.Put($"users/{user.UserId}", dto);
        }

        // Activate user
        public bool ActivateUser(int userId)
        {
            var user = GetUserById(userId);
            if (user == null) return false;

            user.IsActive = true;
            return UpdateUser(user);
        }

        // Deactivate user
        public bool DeactivateUser(int userId)
        {
            // API Delete is soft delete
            return ApiClient.Instance.Delete($"users/{userId}");
        }

        // Change password
        public bool ChangePassword(int userId, string newPassword)
        {
            // API expects just the string in body? Contoller: [FromBody] string newPassword
            // Need to ensure ApiClient sends it as JSON string or raw. 
            // ApiClient.Post/Put serializes object. "string" serialized is "password".
            return ApiClient.Instance.Put($"users/{userId}/change-password", newPassword);
        }

        // Check if email exists
        public bool EmailExists(string email)
        {
            return GetUserByEmail(email) != null;
        }

        private User MapToEntity(UserDto dto)
        {
            // Mapping RoleName to RoleId (Approximation for legacy support)
            // Ideally we fetch Role from API or have a standardized mapping.
            int roleId = 3; // Default User
            if (dto.RoleName.Equals("Admin", StringComparison.OrdinalIgnoreCase)) roleId = 1;
            else if (dto.RoleName.Equals("Manager", StringComparison.OrdinalIgnoreCase)) roleId = 2;

            return new User
            {
                UserId = dto.UserId,
                UserEmail = dto.UserEmail,
                FullName = dto.FullName,
                BranchName = dto.BranchName,
                IsActive = dto.IsActive,
                CreatedDate = dto.CreatedDate,
                RoleId = roleId,
                Role = new Role { RoleId = roleId, RoleName = dto.RoleName }
            };
        }
    }
}
