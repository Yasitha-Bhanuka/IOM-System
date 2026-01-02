using System;
using System.Collections.Generic;
using InventoryManagementSystem.DAL;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.BL
{
    public class UserService
    {
        private UserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        // Get all users
        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        // Get active users
        public List<User> GetActiveUsers()
        {
            return _userRepository.GetActiveUsers();
        }

        // Get user by ID
        public User GetUserById(int userId)
        {
            return _userRepository.GetUserById(userId);
        }

        // Get user by email
        public User GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }

        // Get users by role
        public List<User> GetUsersByRole(int roleId)
        {
            return _userRepository.GetUsersByRole(roleId);
        }

        // Get users by branch
        public List<User> GetUsersByBranch(string branchName)
        {
            return _userRepository.GetUsersByBranch(branchName);
        }

        // Update user
        public bool UpdateUser(User user)
        {
            return _userRepository.UpdateUser(user);
        }

        // Activate user
        public bool ActivateUser(int userId)
        {
            return _userRepository.ActivateUser(userId);
        }

        // Deactivate user
        public bool DeactivateUser(int userId)
        {
            return _userRepository.DeactivateUser(userId);
        }

        // Change password
        public bool ChangePassword(int userId, string newPassword)
        {
            try
            {
                var user = _userRepository.GetUserById(userId);
                if (user == null)
                {
                    return false;
                }

                var authService = new AuthenticationService();
                string salt = authService.GenerateSalt();
                string passwordHash = authService.HashPassword(newPassword, salt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = salt;

                return _userRepository.UpdateUser(user);
            }
            catch
            {
                return false;
            }
        }

        // Check if email exists
        public bool EmailExists(string email)
        {
            return _userRepository.EmailExists(email);
        }
    }
}
