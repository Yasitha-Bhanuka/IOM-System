using IOMSystem.Application.Interfaces;
using IOMSystem.Domain.Entities;
using IOMSystem.Domain.Interfaces;
using IOMSystem.Contract.DTOs;

namespace IOMSystem.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUserRepository _userRepository;

    public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IUserRepository userRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _userRepository = userRepository;
    }

    public async Task<OrderDto?> CreateOrderAsync(CreateOrderDto createOrderDto)
    {
        // 1. Validate User
        var user = await _userRepository.GetByIdAsync(createOrderDto.UserId);
        if (user == null) return null; // Or throw exception

        var orderItems = new List<OrderItem>();
        decimal totalAmount = 0;

        // 2. Process Items and Check Stock
        foreach (var itemDto in createOrderDto.Items)
        {
            var product = await _productRepository.GetBySkuAsync(itemDto.SKU);
            if (product == null || !product.IsActive) return null; // Product not found/inactive

            if (product.StockQuantity < itemDto.Quantity)
                return null; // Insufficient stock

            // Reduce Stock
            product.StockQuantity -= itemDto.Quantity;
            product.LastUpdatedDate = DateTime.Now;
            await _productRepository.UpdateAsync(product);

            // Create Order Item
            var orderItem = new OrderItem
            {
                SKU = product.SKU,
                ProductName = product.ProductName,
                Quantity = itemDto.Quantity,
                UnitPrice = product.Price,
                Subtotal = product.Price * itemDto.Quantity
            };

            orderItems.Add(orderItem);
            totalAmount += orderItem.Subtotal;
        }

        // 3. Create Order
        var order = new Order
        {
            UserId = createOrderDto.UserId,
            OrderDate = DateTime.Now,
            TotalAmount = totalAmount,
            Status = "Pending",
            Notes = createOrderDto.Notes,
            CreatedDate = DateTime.Now,
            OrderItems = orderItems
        };

        // 4. Save Order (Cascade should save items)
        await _orderRepository.AddAsync(order);

        return ToOrderDto(order);
    }

    public async Task<List<OrderDto>> GetOrdersByUserIdAsync(int userId)
    {
        var orders = await _orderRepository.GetByUserIdAsync(userId);
        return orders.Select(ToOrderDto).ToList();
    }

    public async Task<List<OrderDto>> GetAllOrdersAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return orders.Select(ToOrderDto).ToList();
    }

    public async Task<OrderDto?> GetOrderByIdAsync(int orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        return order == null ? null : ToOrderDto(order);
    }

    private static OrderDto ToOrderDto(Order order)
    {
        return new OrderDto
        {
            OrderId = order.OrderId,
            UserId = order.UserId,
            UserEmail = order.User?.UserEmail ?? "Unknown",
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            Status = order.Status,
            Notes = order.Notes,
            OrderItems = order.OrderItems.Select(oi => new OrderItemDto
            {
                SKU = oi.SKU,
                ProductName = oi.ProductName,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice,
                Subtotal = oi.Subtotal
            }).ToList()
        };
    }
}
