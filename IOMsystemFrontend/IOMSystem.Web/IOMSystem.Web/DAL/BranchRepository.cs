using System;
using System.Collections.Generic;
using System.Linq;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DAL
{
    public class BranchRepository : IDisposable
    {
        private InventoryDbContext _context;

        public BranchRepository()
        {
            _context = new InventoryDbContext();
        }

        public BranchRepository(InventoryDbContext context)
        {
            _context = context;
        }

        // Get branch by ID
        public Branch GetBranchById(int branchId)
        {
            return _context.Branches.Find(branchId);
        }

        // Get branch by name
        public Branch GetBranchByName(string branchName)
        {
            return _context.Branches
                .FirstOrDefault(b => b.BranchName.ToLower() == branchName.ToLower());
        }

        // Get branch by code
        public Branch GetBranchByCode(string branchCode)
        {
            return _context.Branches
                .FirstOrDefault(b => b.BranchCode.ToLower() == branchCode.ToLower());
        }

        // Get all branches
        public List<Branch> GetAllBranches()
        {
            return _context.Branches
                .OrderBy(b => b.BranchName)
                .ToList();
        }

        // Get active branches
        public List<Branch> GetActiveBranches()
        {
            return _context.Branches
                .Where(b => b.IsActive)
                .OrderBy(b => b.BranchName)
                .ToList();
        }

        // Create branch
        public bool CreateBranch(Branch branch)
        {
            try
            {
                _context.Branches.Add(branch);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Update branch
        public bool UpdateBranch(Branch branch)
        {
            try
            {
                _context.Entry(branch).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Activate branch
        public bool ActivateBranch(int branchId)
        {
            try
            {
                var branch = _context.Branches.Find(branchId);
                if (branch != null)
                {
                    branch.IsActive = true;
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

        // Deactivate branch
        public bool DeactivateBranch(int branchId)
        {
            try
            {
                var branch = _context.Branches.Find(branchId);
                if (branch != null)
                {
                    branch.IsActive = false;
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

        // Check if branch name exists
        public bool BranchNameExists(string branchName)
        {
            return _context.Branches.Any(b => b.BranchName.ToLower() == branchName.ToLower());
        }

        // Check if branch code exists
        public bool BranchCodeExists(string branchCode)
        {
            return _context.Branches.Any(b => b.BranchCode.ToLower() == branchCode.ToLower());
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
