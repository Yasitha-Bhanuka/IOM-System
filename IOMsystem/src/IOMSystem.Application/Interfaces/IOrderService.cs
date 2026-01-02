using IOMSystem.Application.DTOs;

namespace IOMSystem.Application.Interfaces;

public interface IOrderService
{
    Task<OrderDto?> CreateOrderAsync(CreateOrderDto createOrderDto);
    Task<List<OrderDto>> GetOrdersByUserIdAsync(int userId);
    Task<List<OrderDto>> GetAllOrdersAsync();
    Task<OrderDto?> GetOrderByIdAsync(int orderId);
}
