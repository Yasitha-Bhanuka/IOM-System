namespace IOMSystem.Application.DTOs;

public class RegistrationResponseDto
{
    public int RequestId { get; set; }
    public string UserEmail { get; set; }
    public string FullName { get; set; }
    public string BranchName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime RequestDate { get; set; }
    public string Status { get; set; }
    public int? ActionByUserId { get; set; }
    public string RejectionReason { get; set; }
}

public class CreateRegistrationRequestDto
{
    public string UserEmail { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
    public string BranchName { get; set; }
    public string PhoneNumber { get; set; }
}

public class RejectRegistrationDto
{
    public int ActionByUserId { get; set; }
    public string RejectionReason { get; set; }
}
