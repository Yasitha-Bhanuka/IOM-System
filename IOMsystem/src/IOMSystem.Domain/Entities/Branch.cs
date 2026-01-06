using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOMSystem.Domain.Entities;

[Table("Branches")]
public class Branch
{
    [Key]
    [StringLength(20)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string BranchCode { get; set; } = default!;

    [Required, StringLength(100)]
    public string BranchName { get; set; } = default!;

    [StringLength(200)]
    public string? Address { get; set; }

    [StringLength(50)]
    public string? City { get; set; }

    [StringLength(50)]
    public string? State { get; set; }

    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public ICollection<Stationary> Stationaries { get; set; } = new List<Stationary>();

    public ICollection<UserRegistrationRequest> UserRegistrationRequests { get; set; }
        = new List<UserRegistrationRequest>();
}
