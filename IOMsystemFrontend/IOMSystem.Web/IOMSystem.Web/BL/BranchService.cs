using System.Collections.Generic;
using InventoryManagementSystem.DAL;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.BL
{
    public class BranchService
    {
        private BranchRepository _branchRepository;

        public BranchService()
        {
            _branchRepository = new BranchRepository();
        }

        // Get all branches
        public List<Branch> GetAllBranches()
        {
            return _branchRepository.GetAllBranches();
        }

        // Get active branches
        public List<Branch> GetActiveBranches()
        {
            return _branchRepository.GetActiveBranches();
        }

        // Get branch by ID
        public Branch GetBranchById(int branchId)
        {
            return _branchRepository.GetBranchById(branchId);
        }

        // Get branch by name
        public Branch GetBranchByName(string branchName)
        {
            return _branchRepository.GetBranchByName(branchName);
        }

        // Get branch by code
        public Branch GetBranchByCode(string branchCode)
        {
            return _branchRepository.GetBranchByCode(branchCode);
        }

        // Create branch
        public bool CreateBranch(Branch branch)
        {
            // Check if branch name or code already exists
            if (_branchRepository.BranchNameExists(branch.BranchName) ||
                _branchRepository.BranchCodeExists(branch.BranchCode))
            {
                return false;
            }

            return _branchRepository.CreateBranch(branch);
        }

        // Update branch
        public bool UpdateBranch(Branch branch)
        {
            return _branchRepository.UpdateBranch(branch);
        }

        // Activate branch
        public bool ActivateBranch(int branchId)
        {
            return _branchRepository.ActivateBranch(branchId);
        }

        // Deactivate branch
        public bool DeactivateBranch(int branchId)
        {
            return _branchRepository.DeactivateBranch(branchId);
        }

        // Check if branch name exists
        public bool BranchNameExists(string branchName)
        {
            return _branchRepository.BranchNameExists(branchName);
        }

        // Check if branch code exists
        public bool BranchCodeExists(string branchCode)
        {
            return _branchRepository.BranchCodeExists(branchCode);
        }
    }
}
