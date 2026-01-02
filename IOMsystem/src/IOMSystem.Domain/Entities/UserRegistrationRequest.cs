using System.ComponentModel.DataAnnotations;

namespace IOMSystem.Domain.Entities;

public class UserRegistrationRequest
{
    [Key]
    public int RequestId { get; set; }

    [Required]
    [StringLength(100)]
    public string UserEmail { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    [Required]
    public string PasswordSalt { get; set; }

    [StringLength(100)]
    public string FullName { get; set; }

    [StringLength(50)]
    public string BranchName { get; set; }

    [StringLength(20)]
    public string PhoneNumber { get; set; }

    public DateTime RequestDate { get; set; }

    [StringLength(20)]
    public string Status { get; set; } // Pending, Approved, Rejected

    public int? ActionByUserId { get; set; }
    public DateTime? ActionDate { get; set; }
    public string RejectionReason { get; set; }
}
