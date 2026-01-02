using System.Collections.Generic;
using System.Linq;
using InventoryManagementSystem.Helpers;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.BL
{
    public class BranchService
    {
        public BranchService()
        {
        }

        // Get all branches
        public List<Branch> GetAllBranches()
        {
            var dtos = ApiClient.Instance.Get<List<BranchDto>>("branches");
            if (dtos == null) return new List<Branch>();
            return dtos.Select(MapToEntity).ToList();
        }

        // Get active branches
        public List<Branch> GetActiveBranches()
        {
            var all = GetAllBranches();
            return all.Where(b => b.IsActive).ToList();
        }

        // Get branch by ID
        public Branch GetBranchById(int branchId)
        {
            var dto = ApiClient.Instance.Get<BranchDto>($"branches/{branchId}");
            return dto != null ? MapToEntity(dto) : null;
        }

        // Get branch by name - Client side filter for now
        public Branch GetBranchByName(string branchName)
        {
            var all = GetAllBranches();
            return all.FirstOrDefault(b => b.BranchName.Equals(branchName, System.StringComparison.OrdinalIgnoreCase));
        }

        // Get branch by code
        public Branch GetBranchByCode(string branchCode)
        {
            var all = GetAllBranches();
            return all.FirstOrDefault(b => b.BranchCode.Equals(branchCode, System.StringComparison.OrdinalIgnoreCase));
        }

        // Create branch
        public bool CreateBranch(Branch branch)
        {
            var dto = new BranchDto
            {
                BranchName = branch.BranchName,
                BranchCode = branch.BranchCode,
                Address = branch.Address,
                City = branch.City,
                State = branch.State,
                PhoneNumber = branch.PhoneNumber,
                IsActive = branch.IsActive
            };
            // Assuming POST returns ID or bool. ApiClient.Post returns bool currently unless I use Post<T,R>.
            // We can just return success/fail
            return ApiClient.Instance.Post("branches", dto);
        }

        // Update branch
        public bool UpdateBranch(Branch branch)
        {
            var dto = new BranchDto
            {
                BranchId = branch.BranchId, // Important for ID check
                BranchName = branch.BranchName,
                BranchCode = branch.BranchCode,
                Address = branch.Address,
                City = branch.City,
                State = branch.State,
                PhoneNumber = branch.PhoneNumber,
                IsActive = branch.IsActive
            };
            return ApiClient.Instance.Put($"branches/{branch.BranchId}", dto);
        }

        // Activate branch
        public bool ActivateBranch(int branchId)
        {
            var branch = GetBranchById(branchId);
            if (branch == null) return false;
            branch.IsActive = true;
            return UpdateBranch(branch);
        }

        // Deactivate branch
        public bool DeactivateBranch(int branchId)
        {
            var branch = GetBranchById(branchId);
            if (branch == null) return false;
            branch.IsActive = false;
            return UpdateBranch(branch);
        }

        // Check if branch name exists
        public bool BranchNameExists(string branchName)
        {
            return GetBranchByName(branchName) != null;
        }

        // Check if branch code exists
        public bool BranchCodeExists(string branchCode)
        {
            return GetBranchByCode(branchCode) != null;
        }

        private Branch MapToEntity(BranchDto dto)
        {
            return new Branch
            {
                BranchId = dto.BranchId,
                BranchName = dto.BranchName,
                BranchCode = dto.BranchCode,
                Address = dto.Address,
                City = dto.City ?? "", // Handle potential nulls
                State = dto.State ?? "",
                PhoneNumber = dto.PhoneNumber,
                IsActive = dto.IsActive
            };
        }
    }
}
