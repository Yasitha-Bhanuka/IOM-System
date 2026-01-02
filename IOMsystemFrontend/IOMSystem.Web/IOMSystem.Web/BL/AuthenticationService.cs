using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using InventoryManagementSystem.DAL;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.BL
{
    public class AuthenticationService
    {
        private UserRepository _userRepository;

        public AuthenticationService()
        {
            _userRepository = new UserRepository();
        }

        // Validate user login
        public User ValidateLogin(string email, string branchName, string password)
        {
            try
            {
                // Get user by email
                var user = _userRepository.GetUserByEmail(email);

                if (user == null)
                {
                    return null; // User not found
                }

                // Check if user is active
                if (!user.IsActive)
                {
                    return null; // User is inactive
                }

                // Check if branch matches
                if (!user.BranchName.Equals(branchName, StringComparison.OrdinalIgnoreCase))
                {
                    return null; // Branch mismatch
                }

                // Verify password
                if (VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
                {
                    // Update last login date
                    _userRepository.UpdateLastLoginDate(user.UserId);
                    return user;
                }

                return null; // Invalid password
            }
            catch
            {
                return null;
            }
        }

        // Hash password with salt
        public string HashPassword(string password, string salt)
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

        // Verify password
        public bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            string computedHash = HashPassword(password, storedSalt);
            return computedHash == storedHash;
        }

        // Generate salt
        public string GenerateSalt()
        {
            byte[] saltBytes = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        // Create authentication ticket and set cookie
        public void CreateAuthenticationTicket(User user, bool rememberMe = false)
        {
            var ticket = new FormsAuthenticationTicket(
                1,
                user.UserEmail,
                DateTime.Now,
                DateTime.Now.AddHours(rememberMe ? 24 : 2),
                rememberMe,
                $"{user.UserId}|{user.RoleId}|{user.Role.RoleName}|{user.BranchName}",
                FormsAuthentication.FormsCookiePath
            );

            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true,
                Secure = FormsAuthentication.RequireSSL,
                Path = FormsAuthentication.FormsCookiePath
            };

            if (rememberMe)
            {
                cookie.Expires = DateTime.Now.AddHours(24);
            }

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        // Get current user from authentication ticket
        public User GetCurrentUser()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var identity = HttpContext.Current.User.Identity as FormsIdentity;
                if (identity != null)
                {
                    var ticket = identity.Ticket;
                    var userData = ticket.UserData.Split('|');
                    if (userData.Length >= 1)
                    {
                        int userId = int.Parse(userData[0]);
                        return _userRepository.GetUserById(userId);
                    }
                }
            }
            return null;
        }

        // Logout
        public void Logout()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
        }

        // Check if user is in role
        public bool IsInRole(string roleName)
        {
            var user = GetCurrentUser();
            return user != null && user.Role.RoleName.Equals(roleName, StringComparison.OrdinalIgnoreCase);
        }

        // Get user role name
        public string GetUserRole()
        {
            var user = GetCurrentUser();
            return user?.Role.RoleName;
        }
    }
}
