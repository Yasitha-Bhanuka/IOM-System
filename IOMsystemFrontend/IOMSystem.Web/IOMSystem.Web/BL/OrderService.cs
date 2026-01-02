using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using InventoryManagementSystem.DAL;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.BL
{
    public class OrderService
    {
        private OrderRepository _orderRepository;
        private OrderItemRepository _orderItemRepository;
        private ProductRepository _productRepository;

        public OrderService()
        {
            _orderRepository = new OrderRepository();
            _orderItemRepository = new OrderItemRepository();
            _productRepository = new ProductRepository();
        }

        // US-005: Place order with stock validation
        public int PlaceOrder(int userId, List<OrderItemDto> items, string notes = null)
        {
            try
            {
                // 1. Validate: Order must have at least one item
                if (items == null || items.Count == 0)
                {
                    throw new Exception("Order must contain at least one item");
                }

                // 2. Validate: Check stock availability for each item
                var stockValidation = ValidateStockAvailability(items);
                if (!stockValidation.IsValid)
                {
                    throw new Exception(stockValidation.ErrorMessage);
                }

                // 3. Calculate total using LINQ (US-006)
                decimal totalAmount = CalculateOrderTotal(items);

                // 4. Create Order entity
                var order = new Order
                {
                    UserId = userId,
                    OrderDate = DateTime.Now,
                    TotalAmount = totalAmount,
                    Status = OrderStatus.Pending,
                    Notes = notes,
                    CreatedDate = DateTime.Now
                };

                // 5. Save order and get OrderId
                int orderId = _orderRepository.CreateOrder(order);
                if (orderId == 0)
                {
                    throw new Exception("Failed to create order");
                }

                // 6. Create OrderItem entities
                foreach (var item in items)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = orderId,
                        SKU = item.SKU,
                        ProductName = item.ProductName,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        Subtotal = item.Quantity * item.UnitPrice
                    };

                    _orderItemRepository.CreateOrderItem(orderItem);
                }

                return orderId;
            }
            catch
            {
                return 0;
            }
        }

        // US-006: Calculate order total using LINQ
        public decimal CalculateOrderTotal(List<OrderItemDto> items)
        {
            return items.Sum(item => item.Quantity * item.UnitPrice);
        }

        // US-007: Get all orders
        public List<Order> GetAllOrders()
        {
            return _orderRepository.GetAllOrders();
        }

        // Get orders by user
        public List<Order> GetUserOrders(int userId)
        {
            return _orderRepository.GetOrdersByUserId(userId);
        }

        // Get order details
        public Order GetOrderDetails(int orderId)
        {
            return _orderRepository.GetOrderById(orderId);
        }

        // US-007: Filter orders by status
        public List<Order> FilterOrdersByStatus(string status)
        {
            return _orderRepository.GetOrdersByStatus(status);
        }

        // US-007: Filter orders by date
        public List<Order> FilterOrdersByDate(DateTime startDate, DateTime endDate)
        {
            return _orderRepository.GetOrdersByDateRange(startDate, endDate);
        }

        // Filter orders by branch name
        public List<Order> FilterOrdersByBranch(string branchName)
        {
            return _orderRepository.GetOrdersByBranch(branchName);
        }

        // Validate stock availability for existing order
        public OrderStockValidationResult ValidateStockForOrder(int orderId)
        {
            var order = _orderRepository.GetOrderById(orderId);
            if (order == null)
            {
                return new OrderStockValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Order not found"
                };
            }

            var stockIssues = order.OrderItems
                .Select(item =>
                {
                    var product = _productRepository.GetProductBySKU(item.SKU);
                    return new
                    {
                        Item = item,
                        Product = product,
                        IsInsufficient = product == null || product.StockQuantity < item.Quantity
                    };
                })
                .Where(x => x.IsInsufficient)
                .Select(x => new StockIssue
                {
                    SKU = x.Item.SKU,
                    ProductName = x.Item.ProductName,
                    OrderedQuantity = x.Item.Quantity,
                    AvailableStock = x.Product?.StockQuantity ?? 0,
                    Shortage = x.Item.Quantity - (x.Product?.StockQuantity ?? 0)
                })
                .ToList();

            if (stockIssues.Any())
            {
                var errorMessage = "Cannot approve order: Insufficient stock for the following items:\n";
                foreach (var issue in stockIssues)
                {
                    errorMessage += $"- {issue.ProductName} (SKU: {issue.SKU}): Ordered: {issue.OrderedQuantity}, Available: {issue.AvailableStock}\n";
                }

                return new OrderStockValidationResult
                {
                    IsValid = false,
                    StockIssues = stockIssues,
                    ErrorMessage = errorMessage
                };
            }

            return new OrderStockValidationResult
            {
                IsValid = true,
                ErrorMessage = string.Empty
            };
        }

        // Approve order with automatic stock decrement
        public bool ApproveOrderWithStockDecrement(int orderId, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                // 1. Validate current stock for all items
                var stockValidation = ValidateStockForOrder(orderId);
                if (!stockValidation.IsValid)
                {
                    errorMessage = stockValidation.ErrorMessage;
                    return false;
                }

                var order = _orderRepository.GetOrderById(orderId);
                if (order == null)
                {
                    errorMessage = "Order not found";
                    return false;
                }

                // 2. Begin transaction for atomic operation
                using (var scope = new TransactionScope())
                {
                    // 3. Update order status to Approved
                    if (!_orderRepository.UpdateOrderStatus(orderId, OrderStatus.Approved))
                    {
                        errorMessage = "Failed to update order status";
                        return false;
                    }

                    // 4. Decrement stock for each item
                    foreach (var item in order.OrderItems)
                    {
                        var product = _productRepository.GetProductBySKU(item.SKU);
                        if (product != null)
                        {
                            product.StockQuantity -= item.Quantity;
                            product.LastUpdatedDate = DateTime.Now;

                            if (!_productRepository.UpdateProduct(product))
                            {
                                errorMessage = $"Failed to update stock for product {item.SKU}";
                                return false;
                            }
                        }
                    }

                    // 5. Commit transaction
                    scope.Complete();
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Error approving order: {ex.Message}";
                return false;
            }
        }

        // Cancel order with stock restoration (if needed)
        public bool CancelOrderWithStockRestoration(int orderId)
        {
            try
            {
                var order = _orderRepository.GetOrderById(orderId);
                if (order == null)
                {
                    return false;
                }

                // If order was Approved, restore stock
                if (order.Status == OrderStatus.Approved)
                {
                    using (var scope = new TransactionScope())
                    {
                        // Restore stock for each item
                        foreach (var item in order.OrderItems)
                        {
                            var product = _productRepository.GetProductBySKU(item.SKU);
                            if (product != null)
                            {
                                product.StockQuantity += item.Quantity;
                                product.LastUpdatedDate = DateTime.Now;
                                _productRepository.UpdateProduct(product);
                            }
                        }

                        // Update status to Cancelled
                        _orderRepository.UpdateOrderStatus(orderId, OrderStatus.Cancelled);

                        scope.Complete();
                    }
                }
                else
                {
                    // Just cancel without stock restoration
                    _orderRepository.UpdateOrderStatus(orderId, OrderStatus.Cancelled);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        // US-008: Update order status with validation
        public bool UpdateOrderStatus(int orderId, string newStatus, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                // 1. Validate status is valid
                if (!OrderStatus.IsValidStatus(newStatus))
                {
                    errorMessage = $"Invalid status: {newStatus}";
                    return false;
                }

                var order = _orderRepository.GetOrderById(orderId);
                if (order == null)
                {
                    errorMessage = "Order not found";
                    return false;
                }

                // 2. Validate status transition
                if (!IsValidStatusTransition(order.Status, newStatus))
                {
                    errorMessage = $"Cannot change status from {order.Status} to {newStatus}";
                    return false;
                }

                // 3. If approving, call ApproveOrderWithStockDecrement
                if (newStatus == OrderStatus.Approved)
                {
                    return ApproveOrderWithStockDecrement(orderId, out errorMessage);
                }

                // 4. If cancelling from Approved, restore stock
                if (newStatus == OrderStatus.Cancelled && order.Status == OrderStatus.Approved)
                {
                    return CancelOrderWithStockRestoration(orderId);
                }

                // 5. Update order status normally
                return _orderRepository.UpdateOrderStatus(orderId, newStatus);
            }
            catch (Exception ex)
            {
                errorMessage = $"Error updating status: {ex.Message}";
                return false;
            }
        }

        // Get pending orders count
        public int GetPendingOrdersCount()
        {
            return _orderRepository.GetPendingOrdersCount();
        }

        // Validate stock availability for order placement
        private OrderStockValidationResult ValidateStockAvailability(List<OrderItemDto> items)
        {
            var stockIssues = new List<StockIssue>();

            foreach (var item in items)
            {
                if (item.Quantity <= 0)
                {
                    return new OrderStockValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "Invalid quantity specified"
                    };
                }

                var product = _productRepository.GetProductBySKU(item.SKU);
                if (product == null)
                {
                    stockIssues.Add(new StockIssue
                    {
                        SKU = item.SKU,
                        ProductName = item.ProductName,
                        OrderedQuantity = item.Quantity,
                        AvailableStock = 0,
                        Shortage = item.Quantity
                    });
                }
                else if (product.StockQuantity < item.Quantity)
                {
                    stockIssues.Add(new StockIssue
                    {
                        SKU = item.SKU,
                        ProductName = item.ProductName,
                        OrderedQuantity = item.Quantity,
                        AvailableStock = product.StockQuantity,
                        Shortage = item.Quantity - product.StockQuantity
                    });
                }
            }

            if (stockIssues.Any())
            {
                var errorMessage = "Insufficient stock for the following items:\n";
                foreach (var issue in stockIssues)
                {
                    errorMessage += $"- {issue.ProductName}: Ordered: {issue.OrderedQuantity}, Available: {issue.AvailableStock}\n";
                }

                return new OrderStockValidationResult
                {
                    IsValid = false,
                    StockIssues = stockIssues,
                    ErrorMessage = errorMessage
                };
            }

            return new OrderStockValidationResult
            {
                IsValid = true,
                ErrorMessage = string.Empty
            };
        }

        // Validate status transition
        private bool IsValidStatusTransition(string currentStatus, string newStatus)
        {
            // Cannot change from Shipped or Cancelled
            if (currentStatus == OrderStatus.Shipped || currentStatus == OrderStatus.Cancelled)
            {
                return false;
            }

            // Valid transitions
            if (currentStatus == OrderStatus.Pending)
            {
                return newStatus == OrderStatus.Approved || newStatus == OrderStatus.Cancelled;
            }

            if (currentStatus == OrderStatus.Approved)
            {
                return newStatus == OrderStatus.Shipped || newStatus == OrderStatus.Cancelled;
            }

            return false;
        }
    }

    // DTOs and Helper Classes
    public class OrderItemDto
    {
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class OrderStockValidationResult
    {
        public bool IsValid { get; set; }
        public List<StockIssue> StockIssues { get; set; }
        public string ErrorMessage { get; set; }

        public OrderStockValidationResult()
        {
            StockIssues = new List<StockIssue>();
        }
    }

    public class StockIssue
    {
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public int OrderedQuantity { get; set; }
        public int AvailableStock { get; set; }
        public int Shortage { get; set; }
    }
}
