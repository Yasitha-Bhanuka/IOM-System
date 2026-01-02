using System;
using System.Collections.Generic;
using InventoryManagementSystem.DAL;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.BL
{
    public class RegistrationService
    {
        private RegistrationRequestRepository _requestRepository;
        private UserRepository _userRepository;
        private RoleRepository _roleRepository;

        public RegistrationService()
        {
            _requestRepository = new RegistrationRequestRepository();
            _userRepository = new UserRepository();
            _roleRepository = new RoleRepository();
        }

        // Create registration request
        public bool CreateRegistrationRequest(string email, string password, string branchName, string fullName = null, string phoneNumber = null)
        {
            try
            {
                // Check if user already exists
                if (_userRepository.EmailExists(email))
                {
                    return false; // User already exists
                }

                // Check if pending request already exists
                if (_requestRepository.PendingRequestExists(email))
                {
                    return false; // Pending request already exists
                }

                // Hash password
                var authService = new AuthenticationService();
                string salt = authService.GenerateSalt();
                string passwordHash = authService.HashPassword(password, salt);

                // Create request
                var request = new UserRegistrationRequest
                {
                    UserEmail = email,
                    PasswordHash = passwordHash,
                    PasswordSalt = salt,
                    BranchName = branchName,
                    FullName = fullName,
                    PhoneNumber = phoneNumber,
                    RequestDate = DateTime.Now,
                    Status = "Pending"
                };

                return _requestRepository.CreateRequest(request);
            }
            catch
            {
                return false;
            }
        }

        // Get all pending requests
        public List<UserRegistrationRequest> GetPendingRequests()
        {
            return _requestRepository.GetPendingRequests();
        }

        // Get all requests
        public List<UserRegistrationRequest> GetAllRequests()
        {
            return _requestRepository.GetAllRequests();
        }

        // Get request by ID
        public UserRegistrationRequest GetRequestById(int requestId)
        {
            return _requestRepository.GetRequestById(requestId);
        }

        // Approve registration request
        public bool ApproveRequest(int requestId, int approvedByUserId)
        {
            try
            {
                var request = _requestRepository.GetRequestById(requestId);
                if (request == null || request.Status != "Pending")
                {
                    return false;
                }

                // Get Customer role
                var customerRole = _roleRepository.GetRoleByName("Customer");
                if (customerRole == null)
                {
                    return false;
                }

                // Create user account
                var user = new User
                {
                    UserEmail = request.UserEmail,
                    PasswordHash = request.PasswordHash,
                    PasswordSalt = request.PasswordSalt,
                    BranchName = request.BranchName,
                    FullName = request.FullName,
                    PhoneNumber = request.PhoneNumber,
                    RoleId = customerRole.RoleId,
                    IsActive = true,
                    CreatedDate = DateTime.Now
                };

                if (_userRepository.CreateUser(user))
                {
                    // Update request status
                    return _requestRepository.UpdateRequestStatus(requestId, "Approved", approvedByUserId);
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        // Reject registration request
        public bool RejectRequest(int requestId, int rejectedByUserId, string rejectionReason)
        {
            try
            {
                var request = _requestRepository.GetRequestById(requestId);
                if (request == null || request.Status != "Pending")
                {
                    return false;
                }

                return _requestRepository.UpdateRequestStatus(requestId, "Rejected", rejectedByUserId, rejectionReason);
            }
            catch
            {
                return false;
            }
        }

        // Check if email has pending request
        public bool HasPendingRequest(string email)
        {
            return _requestRepository.PendingRequestExists(email);
        }

        // Delete registration request permanently
        public bool DeleteRequest(int requestId)
        {
            try
            {
                return _requestRepository.DeleteRequest(requestId);
            }
            catch
            {
                return false;
            }
        }
    }
}
