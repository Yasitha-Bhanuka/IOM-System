using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOMSystem.Web.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        [StringLength(20)]
        public string SKU { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }  // Snapshot at order time

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }  // Snapshot at order time

        [Required]
        public decimal Subtotal { get; set; }  // Calculated: Quantity × UnitPrice

        // Navigation properties
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        [ForeignKey("SKU")]
        public virtual Product Product { get; set; }
    }
}
