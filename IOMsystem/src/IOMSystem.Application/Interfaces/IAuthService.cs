using IOMSystem.Application.DTOs;
using IOMSystem.Domain.Entities;

namespace IOMSystem.Application.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(User user);
    }
}
