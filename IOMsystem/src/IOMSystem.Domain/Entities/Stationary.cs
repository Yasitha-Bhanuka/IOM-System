using System.ComponentModel.DataAnnotations;

namespace IOMSystem.Domain.Entities;

public class Stationary
{
    [Key]
    [Required]
    [StringLength(20)]
    public string LocationCode { get; set; }  // Primary Key: ST1-A, ST1-B, etc.

    [StringLength(500)]
    public string Description { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }
}
