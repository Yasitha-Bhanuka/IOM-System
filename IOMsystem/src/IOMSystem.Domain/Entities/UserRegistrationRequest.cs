using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOMSystem.Domain.Entities;

[Table("UserRegistrationRequests")]
public class UserRegistrationRequest
{
    [Key]
    public int RequestId { get; set; }

    [Required, StringLength(100)]
    public string UserEmail { get; set; } = default!;

    [Required]
    public string PasswordHash { get; set; } = default!;

    [Required]
    public string PasswordSalt { get; set; } = default!;

    [StringLength(100)]
    public string? FullName { get; set; }

    [StringLength(20)]
    public string? BranchCode { get; set; }

    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    public DateTime RequestDate { get; set; } = DateTime.UtcNow;

    [StringLength(20)]
    public string Status { get; set; } = "Pending";

    public int? ActionByUserId { get; set; }

    public DateTime? ActionDate { get; set; }

    public string? RejectionReason { get; set; }
}
