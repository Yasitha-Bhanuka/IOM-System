using IOMSystem.Application.DTOs;

namespace IOMSystem.Application.Interfaces;

public interface IProductService
{
    Task<bool> CreateProductAsync(ProductDto productDto);
    Task<bool> UpdateProductAsync(string sku, ProductDto productDto);
    Task<List<ProductDto>> GetAllProductsAsync();
    Task<ProductDto> GetProductBySkuAsync(string sku);
    Task<bool> DeleteProductAsync(string sku);
    Task<string> GetSuggestedProductIdAsync(string locationCode);
}
