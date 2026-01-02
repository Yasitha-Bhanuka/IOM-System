using System;

namespace InventoryManagementSystem.Helpers
{
    public class ProductDto
    {
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public string LocationCode { get; set; }
        public string ProductID { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int MinStockThreshold { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }

    public class OrderItemDto
    {
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
    }

    public class CreateOrderDto
    {
        public int UserId { get; set; }
        public List<CreateOrderItemDto> Items { get; set; }
        public string Notes { get; set; }
    }

    public class CreateOrderItemDto
    {
        public string SKU { get; set; }
        public int Quantity { get; set; }
    }
}
