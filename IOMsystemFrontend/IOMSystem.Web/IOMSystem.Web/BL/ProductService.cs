using System;
using System.Collections.Generic;
using System.Linq;
using IOMSystem.Web.Helpers;
using IOMSystem.Web.Models;

namespace IOMSystem.Web.BL
{
    public class ProductService
    {
        public ProductService()
        {
        }

        public List<Product> GetAllProducts()
        {
            var productDtos = ApiClient.Instance.Get<List<IOMSystem.Web.Helpers.ProductDto>>("products");
            if (productDtos == null) return new List<Product>();

            return productDtos.Select(MapToEntity).ToList();
        }

        public Product GetProductBySKU(string sku)
        {
            var products = GetAllProducts();
            return products.FirstOrDefault(p => p.SKU == sku);
        }

        public bool CreateProduct(string productName, string locationCode, string productID, decimal price, int stockQuantity, int minStockThreshold)
        {
            var dto = new IOMSystem.Web.Helpers.ProductDto
            {
                ProductName = productName,
                LocationCode = locationCode,
                ProductID = productID,
                Price = price,
                StockQuantity = stockQuantity,
                MinStockThreshold = minStockThreshold,
                IsActive = true
            };

            return ApiClient.Instance.Post("products", dto);
        }

        public bool UpdateProduct(string sku, string productName, decimal price, int stockQuantity, int minStockThreshold)
        {
            var product = GetProductBySKU(sku);
            if (product == null) return false;

            var dto = new IOMSystem.Web.Helpers.ProductDto
            {
                SKU = product.SKU,
                ProductName = productName,
                LocationCode = product.LocationCode,
                ProductID = product.ProductID,
                Price = price,
                StockQuantity = stockQuantity,
                MinStockThreshold = minStockThreshold,
                IsActive = product.IsActive,
                CreatedDate = product.CreatedDate
            };

            return ApiClient.Instance.Put($"products/{sku}", dto);
        }

        public bool DeleteProduct(string sku)
        {
            return ApiClient.Instance.Delete($"products/{sku}");
        }

        public bool ActivateProduct(string sku)
        {
            var product = GetProductBySKU(sku);
            if (product == null) return false;

            var dto = new IOMSystem.Web.Helpers.ProductDto
            {
                SKU = product.SKU,
                ProductName = product.ProductName,
                LocationCode = product.LocationCode,
                ProductID = product.ProductID,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                MinStockThreshold = product.MinStockThreshold,
                IsActive = true,
                CreatedDate = product.CreatedDate
            };

            return ApiClient.Instance.Put($"products/{sku}", dto);
        }

        public List<Product> SearchProducts(string searchTerm)
        {
            var all = GetAllProducts();
            if (string.IsNullOrWhiteSpace(searchTerm)) return all;
            return all.Where(p => p.ProductName.ToLower().Contains(searchTerm.ToLower()) || p.SKU.ToLower().Contains(searchTerm.ToLower())).ToList();
        }

        public List<Product> GetActiveProducts()
        {
            return GetAllProducts().Where(p => p.IsActive).ToList();
        }

        private Product MapToEntity(IOMSystem.Web.Helpers.ProductDto dto)
        {
            return new Product
            {
                SKU = dto.SKU,
                ProductName = dto.ProductName,
                LocationCode = dto.LocationCode,
                ProductID = dto.ProductID,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                MinStockThreshold = dto.MinStockThreshold,
                IsActive = dto.IsActive,
                CreatedDate = dto.CreatedDate
            };
        }
    }
}
