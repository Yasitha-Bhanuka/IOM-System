using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOMSystem.Domain.Entities;

[Table("Products")]
public class Product
{
    [Key]
    [StringLength(20)]
    public string SKU { get; set; } = default!;

    [Required, StringLength(100)]
    public string ProductName { get; set; } = default!;

    [Required, StringLength(20)]
    public string LocationCode { get; set; } = default!;

    [Required, StringLength(50)]
    public string ExternalProductCode { get; set; } = default!;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public int MinStockThreshold { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public DateTime? LastUpdatedDate { get; set; }

    public Stationary Stationary { get; set; } = default!;
}
