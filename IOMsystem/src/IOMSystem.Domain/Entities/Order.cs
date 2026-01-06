using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOMSystem.Domain.Entities;

[Table("Orders")]
public class Order
{
    [Key]
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    [Required, StringLength(20)]
    public string Status { get; set; } = default!;

    [StringLength(500)]
    public string? Notes { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public DateTime? LastUpdatedDate { get; set; }

    public User User { get; set; } = default!;

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
