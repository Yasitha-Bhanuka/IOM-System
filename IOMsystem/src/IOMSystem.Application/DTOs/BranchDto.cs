
namespace IOMSystem.Application.DTOs;

public class BranchDto
{
    public int BranchId { get; set; }
    public string BranchName { get; set; }
    public string BranchCode { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsActive { get; set; }
}
