using System;
using System.Collections.Generic;
using System.Linq;
using IOMSystem.Web.Helpers;
using IOMSystem.Web.Models;

namespace IOMSystem.Web.BL
{
    public class RegistrationService
    {
        public RegistrationService()
        {
        }

        // Create registration request
        public bool CreateRegistrationRequest(string email, string password, string branchName, string fullName = null, string phoneNumber = null)
        {
            var dto = new RegistrationRequestDto
            {
                UserEmail = email,
                Password = password,
                BranchName = branchName,
                FullName = fullName,
                PhoneNumber = phoneNumber,
                Status = "Pending"
            };

            try
            {
                ApiClient.Instance.Post<string, RegistrationRequestDto>("registrations", dto);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Get all pending requests
        public List<UserRegistrationRequest> GetPendingRequests()
        {
            var dtos = ApiClient.Instance.Get<List<RegistrationRequestDto>>("registrations/pending");
            if (dtos == null) return new List<UserRegistrationRequest>();
            return dtos.Select(MapToEntity).ToList();
        }

        // Get all requests
        public List<UserRegistrationRequest> GetAllRequests()
        {
            var dtos = ApiClient.Instance.Get<List<RegistrationRequestDto>>("registrations");
            if (dtos == null) return new List<UserRegistrationRequest>();
            return dtos.Select(MapToEntity).ToList();
        }

        // Get request by ID
        public UserRegistrationRequest GetRequestById(int requestId)
        {
            // Requires endpoint GET /api/registrations/{id}
            var dto = ApiClient.Instance.Get<RegistrationRequestDto>($"registrations/{requestId}");
            return dto != null ? MapToEntity(dto) : null;
        }

        // Approve registration request
        public bool ApproveRequest(int requestId, int approvedByUserId)
        {
            try
            {
                ApiClient.Instance.Post<string, int>($"registrations/approve/{requestId}", approvedByUserId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Reject registration request
        public bool RejectRequest(int requestId, int rejectedByUserId, string rejectionReason)
        {
            var dto = new RegistrationRequestDto
            {
                ActionByUserId = rejectedByUserId,
                RejectionReason = rejectionReason
            };

            try
            {
                ApiClient.Instance.Post<string, RegistrationRequestDto>($"registrations/reject/{requestId}", dto);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Check if email has pending request
        public bool HasPendingRequest(string email)
        {
            var pending = GetPendingRequests();
            return pending.Any(r => r.UserEmail.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        // Delete registration request permanently
        public bool DeleteRequest(int requestId)
        {
            return ApiClient.Instance.Delete($"registrations/{requestId}");
        }

        private UserRegistrationRequest MapToEntity(RegistrationRequestDto dto)
        {
            return new UserRegistrationRequest
            {
                RequestId = dto.RequestId,
                UserEmail = dto.UserEmail,
                FullName = dto.FullName,
                BranchName = dto.BranchName,
                PhoneNumber = dto.PhoneNumber,
                RequestDate = dto.RequestDate,
                Status = dto.Status,
                ActionByUserId = dto.ActionByUserId,
                RejectionReason = dto.RejectionReason
            };
        }
    }
}
