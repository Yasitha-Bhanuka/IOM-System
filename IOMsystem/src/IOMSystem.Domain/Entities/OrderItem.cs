using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOMSystem.Domain.Entities;

[Table("OrderItems")]
public class OrderItem
{
    [Key]
    public int OrderItemId { get; set; }

    public int OrderId { get; set; }

    [Required, StringLength(20)]
    public string SKU { get; set; } = default!;

    [Required, StringLength(100)]
    public string ProductName { get; set; } = default!;

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Subtotal { get; set; }

    public Order Order { get; set; } = default!;
}
