using System;
using System.Collections.Generic;
using System.Text;

namespace IOMSystem.Application.DTOs
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
        public bool IsLowStock { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
