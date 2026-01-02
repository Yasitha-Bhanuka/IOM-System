using IOMSystem.Domain.Entities;

namespace IOMSystem.Domain.Interfaces;

public interface IProductRepository
{
    Task<Product> GetBySkuAsync(string sku);
    Task<List<Product>> GetAllAsync();
    Task<List<Product>> GetByLocationAsync(string locationCode);
    Task<bool> ExistsAsync(string sku);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(string sku);
}
