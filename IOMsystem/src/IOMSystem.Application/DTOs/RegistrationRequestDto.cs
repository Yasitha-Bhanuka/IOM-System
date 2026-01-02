namespace IOMSystem.Application.DTOs;

public class RegistrationRequestDto
{
    public int RequestId { get; set; }
    public string UserEmail { get; set; }
    public string FullName { get; set; }
    public string BranchName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime RequestDate { get; set; }
    public string Status { get; set; }

    // For creation
    public string Password { get; set; }

    // For action
    public int? ActionByUserId { get; set; }
    public string RejectionReason { get; set; }
}
