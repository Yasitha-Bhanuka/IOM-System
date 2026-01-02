using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DAL
{
    public class OrderRepository : IDisposable
    {
        private InventoryDbContext _context;

        public OrderRepository()
        {
            _context = new InventoryDbContext();
        }

        public OrderRepository(InventoryDbContext context)
        {
            _context = context;
        }

        // US-005: Create order
        public int CreateOrder(Order order)
        {
            try
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
                return order.OrderId;
            }
            catch
            {
                return 0;
            }
        }

        // US-007: Get all orders with user, branch, and items
        public List<Order> GetAllOrders()
        {
            return _context.Orders
                .Include("User")
                .Include("OrderItems")
                .OrderBy(o => o.OrderDate)
                .ToList();
        }

        // Get orders by user
        public List<Order> GetOrdersByUserId(int userId)
        {
            return _context.Orders
                .Include("User")
                .Include("OrderItems")
                .Where(o => o.UserId == userId)
                .OrderBy(o => o.OrderDate)
                .ToList();
        }

        // Get order by ID with items
        public Order GetOrderById(int orderId)
        {
            return _context.Orders
                .Include("User")
                .Include("OrderItems")
                .FirstOrDefault(o => o.OrderId == orderId);
        }

        // US-007: Filter orders by status (LINQ)
        public List<Order> GetOrdersByStatus(string status)
        {
            return _context.Orders
                .Include("User")
                .Include("OrderItems")
                .Where(o => o.Status == status)
                .OrderBy(o => o.OrderDate)
                .ToList();
        }

        // US-007: Filter orders by date range (LINQ)
        public List<Order> GetOrdersByDateRange(DateTime startDate, DateTime endDate)
        {
            return _context.Orders
                .Include("User")
                .Include("OrderItems")
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .OrderBy(o => o.OrderDate)
                .ToList();
        }

        // Filter orders by customer branch (LINQ)
        public List<Order> GetOrdersByBranch(string branchName)
        {
            return _context.Orders
                .Include("User")
                .Include("OrderItems")
                .Where(o => o.User.BranchName.Contains(branchName))
                .OrderBy(o => o.OrderDate)
                .ToList();
        }

        // US-008: Update order status
        public bool UpdateOrderStatus(int orderId, string newStatus, string notes = null)
        {
            try
            {
                var order = _context.Orders.Find(orderId);
                if (order == null)
                {
                    return false;
                }

                order.Status = newStatus;
                if (!string.IsNullOrWhiteSpace(notes))
                {
                    order.Notes = notes;
                }
                order.LastUpdatedDate = DateTime.Now;

                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Get order count by status
        public int GetOrderCountByStatus(string status)
        {
            return _context.Orders
                .Where(o => o.Status == status)
                .Count();
        }

        // Get pending orders count
        public int GetPendingOrdersCount()
        {
            return GetOrderCountByStatus(OrderStatus.Pending);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
