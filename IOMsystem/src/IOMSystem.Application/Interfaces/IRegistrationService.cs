using IOMSystem.Application.DTOs;

namespace IOMSystem.Application.Interfaces;

public interface IRegistrationService
{
    Task<bool> CreateRequestAsync(RegistrationRequestDto dto);
    Task<List<RegistrationRequestDto>> GetPendingRequestsAsync();
    Task<List<RegistrationRequestDto>> GetAllRequestsAsync();
    Task<RegistrationRequestDto?> GetRequestByIdAsync(int id);
    Task<bool> ApproveRequestAsync(int requestId, int approvedByUserId);
    Task<bool> RejectRequestAsync(int requestId, int rejectedByUserId, string reason);
    Task<bool> DeleteRequestAsync(int requestId);
}
