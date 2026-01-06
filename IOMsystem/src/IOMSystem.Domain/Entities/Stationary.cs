using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOMSystem.Domain.Entities;

[Table("Stationaries")]
public class Stationary
{
    [Key]
    [StringLength(20)]
    public string LocationCode { get; set; } = default!;

    [StringLength(500)]
    public string? Description { get; set; }

    [Required, StringLength(20)]
    public string BranchCode { get; set; } = default!;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public Branch Branch { get; set; } = default!;

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
