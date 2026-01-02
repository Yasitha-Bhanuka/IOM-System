using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DAL
{
    public class RegistrationRequestRepository : IDisposable
    {
        private InventoryDbContext _context;

        public RegistrationRequestRepository()
        {
            _context = new InventoryDbContext();
        }

        public RegistrationRequestRepository(InventoryDbContext context)
        {
            _context = context;
        }

        // Get request by ID
        public UserRegistrationRequest GetRequestById(int requestId)
        {
            return _context.UserRegistrationRequests
                .Include(r => r.ApprovedByUser)
                .FirstOrDefault(r => r.RequestId == requestId);
        }

        // Get all requests
        public List<UserRegistrationRequest> GetAllRequests()
        {
            return _context.UserRegistrationRequests
                .Include(r => r.ApprovedByUser)
                .OrderByDescending(r => r.RequestDate)
                .ToList();
        }

        // Get pending requests
        public List<UserRegistrationRequest> GetPendingRequests()
        {
            return _context.UserRegistrationRequests
                .Where(r => r.Status == "Pending")
                .OrderBy(r => r.RequestDate)
                .ToList();
        }

        // Get approved requests
        public List<UserRegistrationRequest> GetApprovedRequests()
        {
            return _context.UserRegistrationRequests
                .Include(r => r.ApprovedByUser)
                .Where(r => r.Status == "Approved")
                .OrderByDescending(r => r.ApprovedDate)
                .ToList();
        }

        // Get rejected requests
        public List<UserRegistrationRequest> GetRejectedRequests()
        {
            return _context.UserRegistrationRequests
                .Include(r => r.ApprovedByUser)
                .Where(r => r.Status == "Rejected")
                .OrderByDescending(r => r.ApprovedDate)
                .ToList();
        }

        // Get request by email
        public UserRegistrationRequest GetRequestByEmail(string email)
        {
            return _context.UserRegistrationRequests
                .FirstOrDefault(r => r.UserEmail.ToLower() == email.ToLower() && r.Status == "Pending");
        }

        // Create request
        public bool CreateRequest(UserRegistrationRequest request)
        {
            try
            {
                _context.UserRegistrationRequests.Add(request);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Update request status
        public bool UpdateRequestStatus(int requestId, string status, int? approvedByUserId = null, string rejectionReason = null)
        {
            try
            {
                var request = _context.UserRegistrationRequests.Find(requestId);
                if (request != null)
                {
                    request.Status = status;
                    request.ApprovedByUserId = approvedByUserId;
                    request.ApprovedDate = DateTime.Now;
                    request.RejectionReason = rejectionReason;
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

        // Check if pending request exists for email
        public bool PendingRequestExists(string email)
        {
            return _context.UserRegistrationRequests
                .Any(r => r.UserEmail.ToLower() == email.ToLower() && r.Status == "Pending");
        }

        // Check if email has any request
        public bool EmailHasRequest(string email)
        {
            return _context.UserRegistrationRequests
                .Any(r => r.UserEmail.ToLower() == email.ToLower());
        }

        // Delete registration request permanently
        public bool DeleteRequest(int requestId)
        {
            try
            {
                var request = _context.UserRegistrationRequests.Find(requestId);
                if (request != null)
                {
                    _context.UserRegistrationRequests.Remove(request);
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

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
