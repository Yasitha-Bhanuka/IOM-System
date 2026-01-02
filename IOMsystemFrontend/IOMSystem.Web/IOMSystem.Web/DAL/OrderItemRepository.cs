using System;
using System.Collections.Generic;
using System.Linq;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DAL
{
    public class OrderItemRepository : IDisposable
    {
        private InventoryDbContext _context;

        public OrderItemRepository()
        {
            _context = new InventoryDbContext();
        }

        public OrderItemRepository(InventoryDbContext context)
        {
            _context = context;
        }

        // Create order item
        public bool CreateOrderItem(OrderItem orderItem)
        {
            try
            {
                _context.OrderItems.Add(orderItem);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Get items by order ID
        public List<OrderItem> GetOrderItemsByOrderId(int orderId)
        {
            return _context.OrderItems
                .Where(oi => oi.OrderId == orderId)
                .ToList();
        }

        // Delete order items (when order is cancelled)
        public bool DeleteOrderItems(int orderId)
        {
            try
            {
                var items = _context.OrderItems.Where(oi => oi.OrderId == orderId).ToList();
                foreach (var item in items)
                {
                    _context.OrderItems.Remove(item);
                }
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
