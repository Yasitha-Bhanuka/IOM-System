using System;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Text;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DAL
{
    public class DatabaseInitializer : CreateDatabaseIfNotExists<InventoryDbContext>
    {
        protected override void Seed(InventoryDbContext context)
        {
            // Create default roles
            var adminRole = new Role
            {
                RoleName = "Admin",
                Description = "Administrator with full access",
                CreatedDate = DateTime.Now
            };

            var customerRole = new Role
            {
                RoleName = "Customer",
                Description = "Customer with limited access",
                CreatedDate = DateTime.Now
            };

            var guestRole = new Role
            {
                RoleName = "Guest",
                Description = "Guest with view-only access",
                CreatedDate = DateTime.Now
            };

            context.Roles.Add(adminRole);
            context.Roles.Add(customerRole);
            context.Roles.Add(guestRole);
            context.SaveChanges();

            // Create default branches
            var headOfficeBranch = new Branch
            {
                BranchName = "HEAD_OFFICE",
                BranchCode = "HO001",
                Address = "Main Office Building",
                City = "Colombo",
                State = "Western",
                PhoneNumber = "0112345678",
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            var branch1 = new Branch
            {
                BranchName = "BRANCH_NORTH",
                BranchCode = "BN001",
                Address = "North Branch",
                City = "Jaffna",
                State = "Northern",
                PhoneNumber = "0212345678",
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            var branch2 = new Branch
            {
                BranchName = "BRANCH_SOUTH",
                BranchCode = "BS001",
                Address = "South Branch",
                City = "Galle",
                State = "Southern",
                PhoneNumber = "0912345678",
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            context.Branches.Add(headOfficeBranch);
            context.Branches.Add(branch1);
            context.Branches.Add(branch2);
            context.SaveChanges();

            // Create default admin user
            string adminPassword = "Admin@123";
            string salt = GenerateSalt();
            string passwordHash = HashPassword(adminPassword, salt);

            var adminUser = new User
            {
                UserEmail = "admin@inventory.com",
                PasswordHash = passwordHash,
                PasswordSalt = salt,
                BranchName = "HEAD_OFFICE",
                RoleId = adminRole.RoleId,
                IsActive = true,
                FullName = "System Administrator",
                PhoneNumber = "0771234567",
                CreatedDate = DateTime.Now
            };

            var customerUser = new User
            {
                UserEmail = "customer@inventory.com",
                PasswordHash = passwordHash,
                PasswordSalt = salt,
                BranchName = "BRANCH_SOUTH",
                RoleId = customerRole.RoleId,
                IsActive = true,
                FullName = "Customer",
                PhoneNumber = "0770978098",
                CreatedDate = DateTime.Now
            };

            context.Users.Add(adminUser);
            context.Users.Add(customerUser);
            context.SaveChanges();

            base.Seed(context);
        }

        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        private string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] saltBytes = Convert.FromBase64String(salt);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] combinedBytes = new byte[saltBytes.Length + passwordBytes.Length];

                Buffer.BlockCopy(saltBytes, 0, combinedBytes, 0, saltBytes.Length);
                Buffer.BlockCopy(passwordBytes, 0, combinedBytes, saltBytes.Length, passwordBytes.Length);

                byte[] hashBytes = sha256.ComputeHash(combinedBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
