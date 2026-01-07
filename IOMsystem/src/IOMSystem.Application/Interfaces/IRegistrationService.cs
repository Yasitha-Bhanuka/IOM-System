using IOMSystem.Application.DTOs;

namespace IOMSystem.Application.Interfaces;

public interface IRegistrationService
{
    Task<bool> CreateRequestAsync(CreateRegistrationRequestDto dto);
    Task<List<RegistrationResponseDto>> GetPendingRequestsAsync();
    Task<List<RegistrationResponseDto>> GetAllRequestsAsync();
    Task<RegistrationResponseDto?> GetRequestByIdAsync(int id);
    Task<bool> ApproveRequestAsync(int requestId, int approvedByUserId);
    Task<bool> RejectRequestAsync(int requestId, int rejectedByUserId, string reason);
    Task<bool> DeleteRequestAsync(int requestId);
}
