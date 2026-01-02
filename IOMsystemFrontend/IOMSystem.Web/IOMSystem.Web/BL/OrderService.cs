using System;
using System.Collections.Generic;
using System.Linq;
using IOMSystem.Web.Helpers;
using IOMSystem.Web.Models;

namespace IOMSystem.Web.BL
{
    public class OrderService
    {
        public OrderService()
        {
        }

        // Place order
        public int PlaceOrder(int userId, List<OrderItemDto> items, string notes = null)
        {
            // Convert legacy UI DTO to API DTO.
            var createOrderDto = new IOMSystem.Web.Helpers.CreateOrderDto
            {
                UserId = userId,
                Notes = notes,
                Items = items.Select(i => new IOMSystem.Web.Helpers.CreateOrderItemDto
                {
                    SKU = i.SKU,
                    Quantity = i.Quantity
                }).ToList()
            };

            // Return the OrderId from the response
            var result = ApiClient.Instance.Post<IOMSystem.Web.Helpers.OrderDto, IOMSystem.Web.Helpers.CreateOrderDto>("orders", createOrderDto);
            return result != null ? result.OrderId : 0;
        }

        public List<Order> GetAllOrders()
        {
            var dtos = ApiClient.Instance.Get<List<IOMSystem.Web.Helpers.OrderDto>>("orders");
            if (dtos == null) return new List<Order>();
            return dtos.Select(MapToEntity).ToList();
        }

        public List<Order> GetUserOrders(int userId)
        {
            var dtos = ApiClient.Instance.Get<List<IOMSystem.Web.Helpers.OrderDto>>($"orders/user/{userId}");
            if (dtos == null) return new List<Order>();
            return dtos.Select(MapToEntity).ToList();
        }

        public Order GetOrderDetails(int orderId)
        {
            var dto = ApiClient.Instance.Get<IOMSystem.Web.Helpers.OrderDto>($"orders/{orderId}");
            return dto != null ? MapToEntity(dto) : null;
        }

        // Filtering methods - Perform in memory for now as API might not have all filters yet
        public List<Order> FilterOrdersByStatus(string status)
        {
            var all = GetAllOrders();
            return all.Where(o => o.Status == status).ToList();
        }

        public List<Order> FilterOrdersByDate(DateTime startDate, DateTime endDate)
        {
            var all = GetAllOrders();
            return all.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate).ToList();
        }

        // Stubbed methods that relied on direct DB
        public bool UpdateOrderStatus(int orderId, string newStatus, out string errorMessage)
        {
            // Implementation depends on if API supports status update.
            // Currently API might just support Create/Get.
            // Assuming we won't implement this fully yet or just return false.
            errorMessage = "Not fully implemented in API yet";
            return false;
        }

        private Order MapToEntity(IOMSystem.Web.Helpers.OrderDto dto)
        {
            return new Order
            {
                OrderId = dto.OrderId,
                UserId = dto.UserId,
                OrderDate = dto.OrderDate,
                TotalAmount = dto.TotalAmount,
                Status = dto.Status,
                Notes = dto.Notes,
                OrderItems = dto.OrderItems.Select(i => new OrderItem
                {
                    SKU = i.SKU,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Subtotal = i.UnitPrice * i.Quantity // Recalculate or use provided
                }).ToList()
            };
        }
    }

    // Keep internal DTO if needed or reuse Helpers
    // Legacy code used OrderItemDto inside OrderService namespace usually?
    public class OrderItemDto
    {
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
