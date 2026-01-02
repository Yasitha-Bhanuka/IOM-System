using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DAL
{
    public class UserRepository : IDisposable
    {
        private InventoryDbContext _context;

        public UserRepository()
        {
            _context = new InventoryDbContext();
        }

        public UserRepository(InventoryDbContext context)
        {
            _context = context;
        }

        // Get user by email
        public User GetUserByEmail(string email)
        {
            return _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.UserEmail.ToLower() == email.ToLower());
        }

        // Get user by ID
        public User GetUserById(int userId)
        {
            return _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.UserId == userId);
        }

        // Get all users
        public List<User> GetAllUsers()
        {
            return _context.Users
                .Include(u => u.Role)
                .OrderBy(u => u.UserEmail)
                .ToList();
        }

        // Get users by role
        public List<User> GetUsersByRole(int roleId)
        {
            return _context.Users
                .Include(u => u.Role)
                .Where(u => u.RoleId == roleId)
                .OrderBy(u => u.UserEmail)
                .ToList();
        }

        // Get users by branch
        public List<User> GetUsersByBranch(string branchName)
        {
            return _context.Users
                .Include(u => u.Role)
                .Where(u => u.BranchName == branchName)
                .OrderBy(u => u.UserEmail)
                .ToList();
        }

        // Get active users
        public List<User> GetActiveUsers()
        {
            return _context.Users
                .Include(u => u.Role)
                .Where(u => u.IsActive)
                .OrderBy(u => u.UserEmail)
                .ToList();
        }

        // Create user
        public bool CreateUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Update user
        public bool UpdateUser(User user)
        {
            try
            {
                // Find the existing user in the current context
                var existingUser = _context.Users.Find(user.UserId);
                if (existingUser == null)
                {
                    return false;
                }

                // Update only the properties that can be changed
                // Do NOT update Email, BranchName, RoleId - these should remain fixed
                existingUser.FullName = user.FullName;
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.PasswordHash = user.PasswordHash;
                existingUser.PasswordSalt = user.PasswordSalt;
                existingUser.IsActive = user.IsActive;

                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Update last login date
        public bool UpdateLastLoginDate(int userId)
        {
            try
            {
                var user = _context.Users.Find(userId);
                if (user != null)
                {
                    user.LastLoginDate = DateTime.Now;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        // Activate user
        public bool ActivateUser(int userId)
        {
            try
            {
                var user = _context.Users.Find(userId);
                if (user != null)
                {
                    user.IsActive = true;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        // Deactivate user
        public bool DeactivateUser(int userId)
        {
            try
            {
                var user = _context.Users.Find(userId);
                if (user != null)
                {
                    user.IsActive = false;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        // Check if email exists
        public bool EmailExists(string email)
        {
            return _context.Users.Any(u => u.UserEmail.ToLower() == email.ToLower());
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
