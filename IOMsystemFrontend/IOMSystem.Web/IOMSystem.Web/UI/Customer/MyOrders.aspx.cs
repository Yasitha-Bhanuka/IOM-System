using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using InventoryManagementSystem.BL;
using InventoryManagementSystem.Helpers;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.UI.Customer
{
    public partial class MyOrders : CustomerBasePage
    {
        private OrderService orderService;

        protected void Page_Load(object sender, EventArgs e)
        {
            orderService = new OrderService();

            if (!IsPostBack)
            {
                LoadOrders();
            }
        }

        private void LoadOrders()
        {
            try
            {
                // Get orders for current user
                int userId = CurrentUser.UserId;
                List<Order> orders = orderService.GetUserOrders(userId);

                // Apply status filter if selected
                string statusFilter = ddlStatusFilter.SelectedValue;
                if (!string.IsNullOrWhiteSpace(statusFilter))
                {
                    orders = orders.Where(o => o.Status == statusFilter).ToList();
                }

                gvOrders.DataSource = orders;
                gvOrders.DataBind();
            }
            catch (Exception ex)
            {
                // Log error
                Response.Write("<script>alert('Error loading orders: " + ex.Message + "');</script>");
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            LoadOrders();
        }

        protected void btnClearFilter_Click(object sender, EventArgs e)
        {
            ddlStatusFilter.SelectedIndex = 0;
            LoadOrders();
        }

        protected void btnBackToDashboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx", false);
        }

        protected void gvOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetails")
            {
                int orderId = Convert.ToInt32(e.CommandArgument);
                ShowOrderDetails(orderId);
            }
        }

        private void ShowOrderDetails(int orderId)
        {
            try
            {
                Order order = orderService.GetOrderDetails(orderId);
                if (order != null)
                {
                    pnlOrderDetails.Visible = true;

                    lblOrderId.Text = order.OrderId.ToString();
                    lblOrderDate.Text = order.OrderDate.ToString("MMMM dd, yyyy hh:mm tt");
                    lblOrderStatus.Text = GetStatusBadge(order.Status);
                    lblOrderNotes.Text = string.IsNullOrWhiteSpace(order.Notes) ? "None" : order.Notes;
                    lblOrderTotal.Text = order.TotalAmount.ToString("F2");

                    gvOrderItems.DataSource = order.OrderItems;
                    gvOrderItems.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error loading order details: " + ex.Message + "');</script>");
            }
        }

        protected void btnCloseDetails_Click(object sender, EventArgs e)
        {
            pnlOrderDetails.Visible = false;
        }

        // Helper method to generate status badge
        protected string GetStatusBadge(string status)
        {
            switch (status)
            {
                case "Pending":
                    return "<span class='badge-pending'>Pending</span>";
                case "Approved":
                    return "<span class='badge-approved'>Approved</span>";
                case "Shipped":
                    return "<span class='badge-shipped'>Shipped</span>";
                case "Cancelled":
                    return "<span class='badge-cancelled'>Cancelled</span>";
                default:
                    return status;
            }
        }
    }
}
