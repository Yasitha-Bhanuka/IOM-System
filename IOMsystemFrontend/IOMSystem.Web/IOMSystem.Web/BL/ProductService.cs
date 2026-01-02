using System;
using System.Collections.Generic;
using InventoryManagementSystem.DAL;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.BL
{
    public class ProductService
    {
        private ProductRepository _productRepository;
        private StationaryRepository _stationaryRepository;

        public ProductService()
        {
            _productRepository = new ProductRepository();
            _stationaryRepository = new StationaryRepository();
        }

        // Create product with SKU generation
        public bool CreateProduct(string productName, string locationCode, string productID, decimal price, int stockQuantity, int minStockThreshold)
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(productName) ||
                    string.IsNullOrWhiteSpace(locationCode) ||
                    string.IsNullOrWhiteSpace(productID))
                {
                    return false;
                }

                if (price <= 0 || stockQuantity < 0 || minStockThreshold < 0)
                {
                    return false;
                }

                // Verify stationary exists and is active
                var stationary = _stationaryRepository.GetStationaryByCode(locationCode);
                if (stationary == null || !stationary.IsActive)
                {
                    return false;
                }

                // Generate SKU
                string sku = locationCode + productID.Trim().ToUpper();

                // Check if SKU already exists
                if (_productRepository.SKUExists(sku))
                {
                    return false;
                }

                // Validate SKU format
                if (!ValidateSKU(sku))
                {
                    return false;
                }

                var product = new Product
                {
                    SKU = sku,
                    ProductName = productName.Trim(),
                    LocationCode = locationCode,
                    ProductID = productID.Trim().ToUpper(),
                    Price = price,
                    StockQuantity = stockQuantity,
                    MinStockThreshold = minStockThreshold,
                    IsActive = true,
                    CreatedDate = DateTime.Now
                };

                return _productRepository.CreateProduct(product);
            }
            catch
            {
                return false;
            }
        }

        // Update product (cannot change SKU, LocationCode, or ProductID)
        public bool UpdateProduct(string sku, string productName, decimal price, int stockQuantity, int minStockThreshold)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(productName) || price <= 0 || stockQuantity < 0 || minStockThreshold < 0)
                {
                    return false;
                }

                var product = _productRepository.GetProductBySKU(sku);
                if (product == null)
                {
                    return false;
                }

                product.ProductName = productName.Trim();
                product.Price = price;
                product.StockQuantity = stockQuantity;
                product.MinStockThreshold = minStockThreshold;

                return _productRepository.UpdateProduct(product);
            }
            catch
            {
                return false;
            }
        }

        // Get all products
        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        // Get active products
        public List<Product> GetActiveProducts()
        {
            return _productRepository.GetActiveProducts();
        }

        // Get product by SKU
        public Product GetProductBySKU(string sku)
        {
            return _productRepository.GetProductBySKU(sku);
        }

        // Get products by location
        public List<Product> GetProductsByLocation(string locationCode)
        {
            return _productRepository.GetProductsByLocation(locationCode);
        }

        // Delete product (soft delete)
        public bool DeleteProduct(string sku)
        {
            return _productRepository.DeleteProduct(sku);
        }

        // Activate product
        public bool ActivateProduct(string sku)
        {
            return _productRepository.ActivateProduct(sku);
        }

        // Get suggested next Product ID
        public string GetSuggestedProductID(string locationCode)
        {
            return _productRepository.GetNextProductID(locationCode);
        }

        // Search products by SKU or Name (for customer view)
        public List<Product> SearchProducts(string searchTerm)
        {
            return _productRepository.SearchProducts(searchTerm);
        }

        // Get low stock products (US-004)
        public List<Product> GetLowStockProducts()
        {
            return _productRepository.GetLowStockProducts();
        }

        // Get count of low stock products (US-004)
        public int GetLowStockCount()
        {
            return _productRepository.GetLowStockCount();
        }

        // Validate SKU format
        private bool ValidateSKU(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku) || sku.Length > 20)
            {
                return false;
            }
            return true;
        }

        // Check if SKU exists
        public bool SKUExists(string sku)
        {
            return _productRepository.SKUExists(sku);
        }
    }
}
