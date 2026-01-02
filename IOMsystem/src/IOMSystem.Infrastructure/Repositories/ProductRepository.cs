using Microsoft.EntityFrameworkCore;
using IOMSystem.Domain.Entities;
using IOMSystem.Domain.Interfaces;
using IOMSystem.Infrastructure.Data;

namespace IOMSystem.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Product> GetBySkuAsync(string sku)
    {
        return await _context.Products
            .Include(p => p.Stationary)
            .FirstOrDefaultAsync(p => p.SKU == sku);
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products
            .Include(p => p.Stationary)
            .ToListAsync();
    }

    public async Task<List<Product>> GetByLocationAsync(string locationCode)
    {
        return await _context.Products
            .Where(p => p.LocationCode == locationCode)
            .Include(p => p.Stationary)
            .ToListAsync();
    }

    public async Task<bool> ExistsAsync(string sku)
    {
        return await _context.Products.AnyAsync(p => p.SKU == sku);
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string sku)
    {
        var product = await GetBySkuAsync(sku);
        if (product != null)
        {
            // Soft delete as per legacy logic
            product.IsActive = false;
            await UpdateAsync(product);
        }
    }
}
