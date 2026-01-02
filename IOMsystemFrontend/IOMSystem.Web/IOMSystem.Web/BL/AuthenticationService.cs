using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Collections.Generic;
using System.Linq; // Added for convenience if needed
using InventoryManagementSystem.Helpers;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.BL
{
    public class AuthenticationService
    {
        public AuthenticationService()
        {
        }

        // Validate user login via API
        public User ValidateLogin(string email, string branchName, string password)
        {
            try
            {
                var loginDto = new InventoryManagementSystem.Helpers.LoginDto
                {
                    Email = email,
                    Password = password
                };

                // Setup helper to call Post with return type since we updated ApiClient
                var userDto = ApiClient.Instance.Post<InventoryManagementSystem.Helpers.UserDto, InventoryManagementSystem.Helpers.LoginDto>("users/login", loginDto);

                if (userDto == null)
                {
                    return null; // Login failed/Invalid credentials
                }

                // Client-side branch validation to maintain legacy behavior
                // The API validates credentials, but we enforce branch restriction here
                if (!string.Equals(userDto.BranchName, branchName, StringComparison.OrdinalIgnoreCase))
                {
                    return null; // Branch mismatch
                }

                if (!userDto.IsActive)
                {
                    return null; // Inactive
                }

                return MapToEntity(userDto);
            }
            catch
            {
                return null;
            }
        }

        // Map API DTO to Legacy User Entity
        private User MapToEntity(InventoryManagementSystem.Helpers.UserDto dto)
        {
            var user = new User
            {
                UserId = dto.UserId,
                UserEmail = dto.UserEmail,
                FullName = dto.FullName,
                BranchName = dto.BranchName,
                IsActive = dto.IsActive,
                CreatedDate = dto.CreatedDate
                // RoleName is a string in DTO. Legacy User had UserId/RoleId and navigation property Role.
                // We will populate RoleId using a helper or assume conventions if needed.
                // Since CreateAuthenticationTicket uses user.Role.RoleName and user.RoleId, we need to populate these.
            };

            // Populate stub Role object for Ticket creation
            user.Role = new Role { RoleName = dto.RoleName };

            // Note: Legacy Ticket creation used user.RoleId. 
            // If the API doesn't return RoleId, we might need to lookup or just use 0 if not critical for display,
            // or modify CreateAuthenticationTicket to rely solely on RoleName.
            // Let's assume for now 0 is fine or we map names to IDs if we hardcoded them.
            // Actually, let's look at the Ticket method: 
            // $"{user.UserId}|{user.RoleId}|{user.Role.RoleName}|{user.BranchName}"

            return user;
        }

        // Create authentication ticket and set cookie (Legacy logic preserved)
        public void CreateAuthenticationTicket(User user, bool rememberMe = false)
        {
            var ticket = new FormsAuthenticationTicket(
                1,
                user.UserEmail,
                DateTime.Now,
                DateTime.Now.AddHours(rememberMe ? 24 : 2),
                rememberMe,
                $"{user.UserId}|{user.RoleId}|{user.Role?.RoleName ?? "User"}|{user.BranchName}",
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
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var identity = HttpContext.Current.User.Identity as FormsIdentity;
                if (identity != null)
                {
                    var ticket = identity.Ticket;
                    var userData = ticket.UserData.Split('|');
                    if (userData.Length >= 4)
                    {
                        return new User
                        {
                            UserId = int.Parse(userData[0]),
                            // RoleId = int.Parse(userData[1]), // Use if needed
                            Role = new Role { RoleName = userData[2] },
                            BranchName = userData[3],
                            UserEmail = identity.Name
                        };
                    }
                }
            }
            return null;
        }

        // Logout
        public void Logout()
        {
            FormsAuthentication.SignOut();
            if (HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session.Clear();
                HttpContext.Current.Session.Abandon();
            }
        }

        // Check if user is in role
        public bool IsInRole(string roleName)
        {
            var user = GetCurrentUser();
            return user != null && user.Role != null && user.Role.RoleName.Equals(roleName, StringComparison.OrdinalIgnoreCase);
        }

        // Get user role name
        public string GetUserRole()
        {
            var user = GetCurrentUser();
            return user?.Role?.RoleName;
        }
    }
}
