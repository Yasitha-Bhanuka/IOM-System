using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOMSystem.Domain.Entities;

public class Stationary
{
    [Key]
    [Required]
    [StringLength(20)]
    public string LocationCode { get; set; }  // Primary Key: ST1-A, ST1-B, etc.

    [StringLength(500)]
    public string Description { get; set; }

    [Required]
    [StringLength(20)]
    public required string BranchCode { get; set; } // FK to Branch

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    [ForeignKey("BranchCode")]
    public virtual Branch? Branch { get; set; }

    public virtual Product? Product { get; set; } // 1:1 Relationship
}
