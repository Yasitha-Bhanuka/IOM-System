using System;
using System.Collections.Generic;
using System.Linq;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DAL
{
    public class ProductRepository : IDisposable
    {
        private InventoryDbContext _context;

        public ProductRepository()
        {
            _context = new InventoryDbContext();
        }

        public ProductRepository(InventoryDbContext context)
        {
            _context = context;
        }

        // Get all products with stationary details
        public List<Product> GetAllProducts()
        {
            return _context.Products
                .Include("Stationary")
                .OrderBy(p => p.SKU)
                .ToList();
        }

        // Get active products only
        public List<Product> GetActiveProducts()
        {
            return _context.Products
                .Include("Stationary")
                .Where(p => p.IsActive)
                .OrderBy(p => p.SKU)
                .ToList();
        }

        // Get product by SKU
        public Product GetProductBySKU(string sku)
        {
            return _context.Products
                .Include("Stationary")
                .FirstOrDefault(p => p.SKU == sku);
        }

        // Get products by stationary location
        public List<Product> GetProductsByLocation(string locationCode)
        {
            return _context.Products
                .Include("Stationary")
                .Where(p => p.LocationCode == locationCode)
                .OrderBy(p => p.SKU)
                .ToList();
        }

        // Check if SKU exists
        public bool SKUExists(string sku)
        {
            return _context.Products.Any(p => p.SKU == sku);
        }

        // Create product
        public bool CreateProduct(Product product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Update product
        public bool UpdateProduct(Product product)
        {
            try
            {
                var existingProduct = _context.Products.Find(product.SKU);
                if (existingProduct == null)
                {
                    return false;
                }

                // Update only editable properties
                existingProduct.ProductName = product.ProductName;
                existingProduct.Price = product.Price;
                existingProduct.StockQuantity = product.StockQuantity;
                existingProduct.MinStockThreshold = product.MinStockThreshold;
                existingProduct.LastUpdatedDate = DateTime.Now;

                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Delete product (soft delete)
        public bool DeleteProduct(string sku)
        {
            try
            {
                var product = _context.Products.Find(sku);
                if (product != null)
                {
                    product.IsActive = false;
                    product.LastUpdatedDate = DateTime.Now;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        // Activate product
        public bool ActivateProduct(string sku)
        {
            try
            {
                var product = _context.Products.Find(sku);
                if (product != null)
                {
                    product.IsActive = true;
                    product.LastUpdatedDate = DateTime.Now;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        // Search products by SKU or Name (for customer view)
        public List<Product> SearchProducts(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return GetActiveProducts();
            }

            searchTerm = searchTerm.Trim().ToLower();

            return _context.Products
                .Include("Stationary")
                .Where(p => p.IsActive &&
                       (p.SKU.ToLower().Contains(searchTerm) ||
                        p.ProductName.ToLower().Contains(searchTerm)))
                .OrderBy(p => p.ProductName)
                .ToList();
        }

        // Get low stock products using LINQ (US-004)
        public List<Product> GetLowStockProducts()
        {
            return _context.Products
                .Include("Stationary")
                .Where(p => p.IsActive && p.StockQuantity < p.MinStockThreshold)
                .OrderBy(p => p.StockQuantity)
                .ToList();
        }

        // Get count of low stock products (US-004)
        public int GetLowStockCount()
        {
            return _context.Products
                .Where(p => p.IsActive && p.StockQuantity < p.MinStockThreshold)
                .Count();
        }

        // Get next available Product ID for a location
        public string GetNextProductID(string locationCode)
        {
            try
            {
                var products = _context.Products
                    .Where(p => p.LocationCode == locationCode)
                    .Select(p => p.ProductID)
                    .ToList();

                if (!products.Any())
                {
                    return "001";
                }

                // Find the highest numeric ID
                int maxId = 0;
                foreach (var id in products)
                {
                    if (int.TryParse(id, out int numericId))
                    {
                        if (numericId > maxId)
                        {
                            maxId = numericId;
                        }
                    }
                }

                // Return next ID with zero-padding
                return (maxId + 1).ToString("D3");
            }
            catch
            {
                return "001";
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
