using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using InventoryManagementSystem.BL;
using InventoryManagementSystem.Helpers;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.UI.Customer
{
    public partial class ViewProducts : CustomerBasePage
    {
        private ProductService productService;
        private OrderService orderService;

        // Shopping cart stored in session
        private List<OrderItemDto> ShoppingCart
        {
            get
            {
                if (Session["ShoppingCart"] == null)
                {
                    Session["ShoppingCart"] = new List<OrderItemDto>();
                }
                return (List<OrderItemDto>)Session["ShoppingCart"];
            }
            set
            {
                Session["ShoppingCart"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            productService = new ProductService();
            orderService = new OrderService();

            if (!IsPostBack)
            {
                LoadProducts();
                UpdateCartDisplay();
            }
        }

        private void LoadProducts(string searchTerm = "", string sortOrder = "")
        {
            try
            {
                List<Product> products;

                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    products = productService.GetActiveProducts();
                }
                else
                {
                    products = productService.SearchProducts(searchTerm);
                }

                // Apply sorting
                if (!string.IsNullOrWhiteSpace(sortOrder))
                {
                    if (sortOrder == "AZ")
                    {
                        products = products.OrderBy(p => p.ProductName).ToList();
                    }
                    else if (sortOrder == "ZA")
                    {
                        products = products.OrderByDescending(p => p.ProductName).ToList();
                    }
                }

                gvProducts.DataSource = products;
                gvProducts.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading products: " + ex.Message, "danger");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            string sortOrder = ViewState["SortOrder"]?.ToString() ?? "";
            LoadProducts(searchTerm, sortOrder);
        }

        protected void btnSortAZ_Click(object sender, EventArgs e)
        {
            ViewState["SortOrder"] = "AZ";
            string searchTerm = txtSearch.Text.Trim();
            LoadProducts(searchTerm, "AZ");
        }

        protected void btnSortZA_Click(object sender, EventArgs e)
        {
            ViewState["SortOrder"] = "ZA";
            string searchTerm = txtSearch.Text.Trim();
            LoadProducts(searchTerm, "ZA");
        }

        protected void btnBackToDashboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx", false);
        }

        protected void btnAddToCart_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "AddToCart")
            {
                string sku = e.CommandArgument.ToString();

                // Find the GridView row
                Button btn = (Button)sender;
                GridViewRow row = (GridViewRow)btn.NamingContainer;

                // Get quantity from textbox
                TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                int quantity;
                if (!int.TryParse(txtQuantity.Text, out quantity) || quantity <= 0)
                {
                    ShowMessage("Please enter a valid quantity", "warning");
                    return;
                }

                // Get product details
                var product = productService.GetProductBySKU(sku);
                if (product == null)
                {
                    ShowMessage("Product not found", "danger");
                    return;
                }

                // Check stock
                if (product.StockQuantity < quantity)
                {
                    ShowMessage($"Insufficient stock. Available: {product.StockQuantity}", "warning");
                    return;
                }

                // Check if item already in cart
                var existingItem = ShoppingCart.FirstOrDefault(item => item.SKU == sku);
                if (existingItem != null)
                {
                    // Update quantity
                    existingItem.Quantity += quantity;
                }
                else
                {
                    // Add new item to cart
                    ShoppingCart.Add(new OrderItemDto
                    {
                        SKU = sku,
                        ProductName = product.ProductName,
                        Quantity = quantity,
                        UnitPrice = product.Price
                    });
                }

                UpdateCartDisplay();
                ShowMessage($"Added {product.ProductName} to cart", "success");
            }
        }

        protected void gvCart_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "RemoveItem")
            {
                string sku = e.CommandArgument.ToString();
                var item = ShoppingCart.FirstOrDefault(i => i.SKU == sku);
                if (item != null)
                {
                    ShoppingCart.Remove(item);
                    UpdateCartDisplay();
                    ShowMessage("Item removed from cart", "info");
                }
            }
        }

        protected void btnClearCart_Click(object sender, EventArgs e)
        {
            ShoppingCart.Clear();
            UpdateCartDisplay();
            ShowMessage("Cart cleared", "info");
        }

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (ShoppingCart.Count == 0)
                {
                    ShowMessage("Your cart is empty", "warning");
                    return;
                }

                // Get current user ID
                int userId = CurrentUser.UserId;

                // Place order
                int orderId = orderService.PlaceOrder(userId, ShoppingCart);

                if (orderId > 0)
                {
                    // Clear cart
                    ShoppingCart.Clear();
                    UpdateCartDisplay();

                    ShowMessage($"Order placed successfully! Order ID: {orderId}", "success");

                    // Redirect to My Orders page after 2 seconds
                    Response.AddHeader("REFRESH", "2;URL=MyOrders.aspx");
                }
                else
                {
                    ShowMessage("Failed to place order. Please try again.", "danger");
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error placing order: " + ex.Message, "danger");
            }
        }

        private void UpdateCartDisplay()
        {
            if (ShoppingCart.Count > 0)
            {
                pnlCart.Visible = true;
                gvCart.DataSource = ShoppingCart;
                gvCart.DataBind();

                // Calculate total
                decimal total = ShoppingCart.Sum(item => item.Quantity * item.UnitPrice);
                lblCartTotal.Text = total.ToString("F2");
            }
            else
            {
                pnlCart.Visible = false;
            }
        }

        private void ShowMessage(string message, string type)
        {
            pnlMessage.Visible = true;
            pnlMessage.CssClass = $"alert alert-{type} mt-3";
            lblMessage.Text = message;
        }

        // Helper method to generate availability badge
        protected string GetAvailabilityBadge(int stockQuantity, int minThreshold)
        {
            if (stockQuantity == 0)
            {
                return "<span class='badge-out-of-stock'>Out of Stock</span>";
            }
            else if (stockQuantity < minThreshold)
            {
                return "<span class='badge-low-stock'>Low Stock</span>";
            }
            else
            {
                return "<span class='badge-available'>Available</span>";
            }
        }
    }
}
