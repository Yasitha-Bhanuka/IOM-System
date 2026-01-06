using IOMSystem.Contracts.DTOs;
using IOMSystem.Application.Interfaces;
using IOMSystem.Domain.Entities;
using IOMSystem.Domain.Interfaces;

namespace IOMSystem.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IStationaryRepository _stationaryRepository;

    public ProductService(IProductRepository productRepository, IStationaryRepository stationaryRepository)
    {
        _productRepository = productRepository;
        _stationaryRepository = stationaryRepository;
    }

    public async Task<bool> CreateProductAsync(ProductDto productDto)
    {
        // Validation (simplified port from legacy)
        if (string.IsNullOrWhiteSpace(productDto.ProductName) ||
            string.IsNullOrWhiteSpace(productDto.LocationCode) ||
            string.IsNullOrWhiteSpace(productDto.ProductID))
            return false;

        if (productDto.Price <= 0 || productDto.StockQuantity < 0 || productDto.MinStockThreshold < 0)
            return false;

        if (!await _stationaryRepository.IsActiveAsync(productDto.LocationCode))
            return false;

        string sku = productDto.LocationCode + productDto.ProductID.Trim().ToUpper();

        if (await _productRepository.ExistsAsync(sku))
            return false;

        var product = new Product
        {
            SKU = sku,
            ProductName = productDto.ProductName.Trim(),
            LocationCode = productDto.LocationCode,
            ExternalProductCode = productDto.ProductID.Trim().ToUpper(),
            Price = productDto.Price,
            StockQuantity = productDto.StockQuantity,
            MinStockThreshold = productDto.MinStockThreshold,
            IsActive = true,
            CreatedDate = DateTime.Now
        };

        await _productRepository.AddAsync(product);
        return true;
    }

    public async Task<bool> UpdateProductAsync(string sku, ProductDto productDto)
    {
        if (string.IsNullOrWhiteSpace(productDto.ProductName) ||
            productDto.Price <= 0 ||
            productDto.StockQuantity < 0 ||
            productDto.MinStockThreshold < 0)
            return false;

        var product = await _productRepository.GetBySkuAsync(sku);
        if (product == null) return false;

        product.ProductName = productDto.ProductName.Trim();
        product.Price = productDto.Price;
        product.StockQuantity = productDto.StockQuantity;
        product.MinStockThreshold = productDto.MinStockThreshold;
        product.LastUpdatedDate = DateTime.Now;

        await _productRepository.UpdateAsync(product);
        return true;
    }

    public async Task<List<ProductDto>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return products.Select(MapToDto).ToList();
    }

    public async Task<ProductDto> GetProductBySkuAsync(string sku)
    {
        var product = await _productRepository.GetBySkuAsync(sku);
        return product != null ? MapToDto(product) : null;
    }

    public async Task<bool> DeleteProductAsync(string sku)
    {
        await _productRepository.DeleteAsync(sku);
        return true;
    }

    public async Task<string> GetSuggestedProductIdAsync(string locationCode)
    {
        // Ideally this logic should be in Repository or Domain Service
        // For now returning a placeholder as strict porting might require complex SQL
        return "001";
    }

    private static ProductDto MapToDto(Product product)
    {
        return new ProductDto
        {
            SKU = product.SKU,
            ProductName = product.ProductName,
            LocationCode = product.LocationCode,
            ProductID = product.ExternalProductCode,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            MinStockThreshold = product.MinStockThreshold,
            IsActive = product.IsActive,
            CreatedDate = product.CreatedDate
        };
    }
}
