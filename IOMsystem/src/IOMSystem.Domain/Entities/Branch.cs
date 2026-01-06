using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOMSystem.Domain.Entities;

[Table("Branches")]
public class Branch
{
    [Key]
    [StringLength(20)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)] // User provides code
    public required string BranchCode { get; set; }

    [Required]
    [StringLength(100)]
    public required string BranchName { get; set; }

    [StringLength(200)]
    public string? Address { get; set; }

    [StringLength(50)]
    public string? City { get; set; }

    [StringLength(50)]
    public string? State { get; set; }

    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
