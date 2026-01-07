using System.Security.Cryptography;
using System.Text;
using IOMSystem.Application.Interfaces;
using IOMSystem.Domain.Entities;
using IOMSystem.Domain.Interfaces;
using IOMSystem.Contract.DTOs;

namespace IOMSystem.Application.Services;

public class RegistrationService : IRegistrationService
{
    private readonly IRegistrationRequestRepository _requestRepository;
    private readonly IUserRepository _userRepository;

    public RegistrationService(IRegistrationRequestRepository requestRepository, IUserRepository userRepository)
    {
        _requestRepository = requestRepository;
        _userRepository = userRepository;
    }

    public async Task<bool> CreateRequestAsync(CreateRegistrationRequestDto dto)
    {
        if (await _userRepository.EmailExistsAsync(dto.UserEmail)) return false;
        if (await _requestRepository.PendingExistsAsync(dto.UserEmail)) return false;

        CreatePasswordHash(dto.Password, out string passwordHash, out string passwordSalt);

        var request = new UserRegistrationRequest
        {
            UserEmail = dto.UserEmail,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            BranchCode = dto.BranchCode,
            FullName = dto.FullName,
            PhoneNumber = dto.PhoneNumber,
            RequestDate = DateTime.Now,
            Status = "Pending"
        };

        await _requestRepository.AddAsync(request);
        return true;
    }

    public async Task<List<RegistrationResponseDto>> GetPendingRequestsAsync()
    {
        var requests = await _requestRepository.GetPendingAsync();
        return requests.Select(MapToDto).ToList();
    }

    public async Task<List<RegistrationResponseDto>> GetAllRequestsAsync()
    {
        var requests = await _requestRepository.GetAllAsync();
        return requests.Select(MapToDto).ToList();
    }

    public async Task<RegistrationResponseDto?> GetRequestByIdAsync(int id)
    {
        var request = await _requestRepository.GetByIdAsync(id);
        return request == null ? null : MapToDto(request);
    }

    // ... Approve method remains mostly same signature wise ...

    public async Task<bool> ApproveRequestAsync(int requestId, int approvedByUserId)
    {
        // ... implementation same ...
        var request = await _requestRepository.GetByIdAsync(requestId);
        if (request == null || request.Status != "Pending") return false;

        // Create User
        var role = await _userRepository.GetRoleByNameAsync("Customer") ?? await _userRepository.GetRoleByNameAsync("User");
        if (role == null) return false;

        var user = new User
        {
            UserEmail = request.UserEmail,
            PasswordHash = request.PasswordHash,
            PasswordSalt = request.PasswordSalt,
            BranchCode = request.BranchCode,
            FullName = request.FullName,
            RoleId = role.RoleId,
            IsActive = true,
            CreatedDate = DateTime.Now
        };

        await _userRepository.AddAsync(user);

        request.Status = "Approved";
        request.ActionByUserId = approvedByUserId;
        request.ActionDate = DateTime.Now;
        await _requestRepository.UpdateAsync(request);

        return true;
    }

    public async Task<bool> RejectRequestAsync(int requestId, int rejectedByUserId, string reason)
    {
        var request = await _requestRepository.GetByIdAsync(requestId);
        if (request == null || request.Status != "Pending") return false;

        request.Status = "Rejected";
        request.ActionByUserId = rejectedByUserId;
        request.RejectionReason = reason;
        request.ActionDate = DateTime.Now;

        await _requestRepository.UpdateAsync(request);
        return true;
    }

    public async Task<bool> DeleteRequestAsync(int requestId)
    {
        var request = await _requestRepository.GetByIdAsync(requestId);
        if (request == null) return false;

        await _requestRepository.DeleteAsync(request);
        return true;
    }

    private RegistrationResponseDto MapToDto(UserRegistrationRequest r)
    {
        return new RegistrationResponseDto
        {
            RequestId = r.RequestId,
            UserEmail = r.UserEmail,
            FullName = r.FullName,
            BranchCode = r.BranchCode,
            PhoneNumber = r.PhoneNumber,
            RequestDate = r.RequestDate,
            Status = r.Status,
            ActionByUserId = r.ActionByUserId,
            RejectionReason = r.RejectionReason
        };
    }

    private void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
    {
        using (var hmac = new HMACSHA256())
        {
            passwordSalt = Convert.ToBase64String(hmac.Key);
            passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }
}
