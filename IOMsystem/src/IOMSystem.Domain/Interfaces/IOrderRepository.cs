using IOMSystem.Domain.Entities;

namespace IOMSystem.Domain.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(int orderId);
    Task<List<Order>> GetAllAsync();
    Task<List<Order>> GetByUserIdAsync(int userId);
    Task AddAsync(Order order);
}
