using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOMSystem.Domain.Entities;

public class Product
{
    [Key]
    [Required]
    [StringLength(20)]
    public string SKU { get; set; }  // Primary Key: ST1-A001, ST1-A002

    [Required]
    [StringLength(100)]
    public required string ProductName { get; set; }

    [Required]
    [StringLength(20)]
    public required string LocationCode { get; set; }  // FK to Stationary

    [Required]
    [StringLength(100)]
    public required string ProductID { get; set; }  // User-entered ID (e.g., 001, 002)

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int StockQuantity { get; set; }

    [Required]
    public int MinStockThreshold { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? LastUpdatedDate { get; set; }

    // Navigation property
    [ForeignKey("LocationCode")]
    public virtual Stationary? Stationary { get; set; }
}
