using System;
using System.Collections.Generic;
using System.Linq;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DAL
{
    public class RoleRepository : IDisposable
    {
        private InventoryDbContext _context;

        public RoleRepository()
        {
            _context = new InventoryDbContext();
        }

        public RoleRepository(InventoryDbContext context)
        {
            _context = context;
        }

        // Get role by ID
        public Role GetRoleById(int roleId)
        {
            return _context.Roles.Find(roleId);
        }

        // Get role by name
        public Role GetRoleByName(string roleName)
        {
            return _context.Roles
                .FirstOrDefault(r => r.RoleName.ToLower() == roleName.ToLower());
        }

        // Get all roles
        public List<Role> GetAllRoles()
        {
            return _context.Roles
                .OrderBy(r => r.RoleName)
                .ToList();
        }

        // Create role
        public bool CreateRole(Role role)
        {
            try
            {
                _context.Roles.Add(role);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Check if role exists
        public bool RoleExists(string roleName)
        {
            return _context.Roles.Any(r => r.RoleName.ToLower() == roleName.ToLower());
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
