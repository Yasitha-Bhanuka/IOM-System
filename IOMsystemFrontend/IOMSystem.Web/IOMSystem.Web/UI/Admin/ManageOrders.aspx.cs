using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using InventoryManagementSystem.BL;
using InventoryManagementSystem.Helpers;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.UI.Admin
{
    public partial class ManageOrders : AdminBasePage
    {
        private OrderService orderService;
        private ProductService productService;

        protected void Page_Load(object sender, EventArgs e)
        {
            orderService = new OrderService();
            productService = new ProductService();

            if (!IsPostBack)
            {
                LoadBranches();
                LoadOrders();
            }
        }

        private void LoadBranches()
        {
            try
            {
                // Get active branches from API via BranchService
                var branchService = new BranchService();
                var branches = branchService.GetActiveBranches();

                // Sort by BranchCode
                branches = branches.OrderBy(b => b.BranchCode).ToList();

                ddlBranchFilter.Items.Clear();
                ddlBranchFilter.Items.Add(new ListItem("All Branches", ""));

                foreach (var branch in branches)
                {
                    string displayText = $"{branch.BranchCode} - {branch.BranchName}";
                    ddlBranchFilter.Items.Add(new ListItem(displayText, branch.BranchName));
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading branches: " + ex.Message, "danger");
            }
        }

        private void LoadOrders()
        {
            try
            {
                List<Order> orders;

                string branchFilter = ddlBranchFilter.SelectedValue;
                string statusFilter = ddlStatusFilter.SelectedValue;

                if (!string.IsNullOrWhiteSpace(branchFilter))
                {
                    orders = orderService.FilterOrdersByBranch(branchFilter);

                    // Apply status filter if both are selected
                    if (!string.IsNullOrWhiteSpace(statusFilter))
                    {
                        orders = orders.Where(o => o.Status == statusFilter).ToList();
                    }
                }
                else if (!string.IsNullOrWhiteSpace(statusFilter))
                {
                    orders = orderService.FilterOrdersByStatus(statusFilter);
                }
                else
                {
                    orders = orderService.GetAllOrders();
                }

                gvOrders.DataSource = orders;
                gvOrders.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading orders: " + ex.Message, "danger");
            }
        }

        protected void gvOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Order order = (Order)e.Row.DataItem;

                // Find the stock status label
                Label lblStockStatus = (Label)e.Row.FindControl("lblStockStatus");

                if (lblStockStatus != null && order.Status == OrderStatus.Pending)
                {
                    // Validate stock for pending orders
                    var stockValidation = orderService.ValidateStockForOrder(order.OrderId);

                    if (stockValidation.IsValid)
                    {
                        lblStockStatus.Text = "<span class='stock-ok'>✓ Stock OK</span>";
                    }
                    else
                    {
                        lblStockStatus.Text = "<span class='stock-error'>✗ Insufficient Stock</span>";
                    }
                }
                else
                {
                    lblStockStatus.Text = "<span class='text-muted'>-</span>";
                }
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            LoadOrders();
        }

        protected void btnClearFilter_Click(object sender, EventArgs e)
        {
            ddlBranchFilter.SelectedIndex = 0;
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
                ShowOrderDetailsPanel(orderId);
            }
        }

        private void ShowOrderDetailsPanel(int orderId)
        {
            try
            {
                var order = orderService.GetOrderDetails(orderId);
                if (order != null)
                {
                    // Show panel
                    pnlOrderDetails.Visible = true;

                    // Populate order information
                    lblOrderId.Text = order.OrderId.ToString();
                    lblCustomerName.Text = order.User?.FullName ?? "N/A";
                    lblBranchName.Text = order.User?.BranchName ?? "N/A";
                    lblOrderDate.Text = order.OrderDate.ToString("MMMM dd, yyyy hh:mm tt");
                    lblOrderStatus.Text = GetStatusBadge(order.Status);
                    lblTotalAmount.Text = order.TotalAmount.ToString("F2");
                    lblNotes.Text = string.IsNullOrWhiteSpace(order.Notes) ? "None" : order.Notes;

                    // Bind order items
                    gvOrderItems.DataSource = order.OrderItems;
                    gvOrderItems.DataBind();

                    // Store order ID and status in ViewState
                    ViewState["CurrentOrderId"] = orderId;
                    ViewState["CurrentOrderStatus"] = order.Status;

                    // Show/hide update status section based on order status
                    if (order.Status == OrderStatus.Shipped || order.Status == OrderStatus.Cancelled)
                    {
                        pnlUpdateStatusSection.Visible = false;
                    }
                    else
                    {
                        pnlUpdateStatusSection.Visible = true;

                        // Validate stock for pending orders
                        if (order.Status == OrderStatus.Pending)
                        {
                            var stockValidation = orderService.ValidateStockForOrder(orderId);
                            if (!stockValidation.IsValid)
                            {
                                pnlStockWarning.Visible = true;
                                lblStockWarning.Text = stockValidation.ErrorMessage.Replace("\n", "<br/>");
                            }
                            else
                            {
                                pnlStockWarning.Visible = false;
                            }
                        }
                        else
                        {
                            pnlStockWarning.Visible = false;
                        }

                        // Populate status dropdown
                        PopulateStatusDropdown(order.Status);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading order details: " + ex.Message, "danger");
            }
        }

        private void PopulateStatusDropdown(string currentStatus)
        {
            ddlNewStatus.Items.Clear();
            ddlNewStatus.Items.Add(new ListItem("Select Status", ""));

            if (currentStatus == OrderStatus.Pending)
            {
                ddlNewStatus.Items.Add(new ListItem("Approved", OrderStatus.Approved));
                ddlNewStatus.Items.Add(new ListItem("Cancelled", OrderStatus.Cancelled));
            }
            else if (currentStatus == OrderStatus.Approved)
            {
                ddlNewStatus.Items.Add(new ListItem("Shipped", OrderStatus.Shipped));
                ddlNewStatus.Items.Add(new ListItem("Cancelled", OrderStatus.Cancelled));
            }
        }

        protected void btnCloseDetails_Click(object sender, EventArgs e)
        {
            pnlOrderDetails.Visible = false;
            pnlStockWarning.Visible = false;
        }

        protected void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            try
            {
                int orderId = Convert.ToInt32(ViewState["CurrentOrderId"]);
                string newStatus = ddlNewStatus.SelectedValue;

                if (string.IsNullOrWhiteSpace(newStatus))
                {
                    ShowMessage("Please select a status", "warning");
                    return;
                }

                string errorMessage;
                bool success = orderService.UpdateOrderStatus(orderId, newStatus, out errorMessage);

                if (success)
                {
                    ShowMessage($"Order #{orderId} status updated to {newStatus} successfully!", "success");
                    pnlOrderDetails.Visible = false;
                    LoadOrders();
                }
                else
                {
                    ShowMessage($"Failed to update status: {errorMessage}", "danger");
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error updating status: " + ex.Message, "danger");
            }
        }

        private void ShowMessage(string message, string type)
        {
            pnlMessage.Visible = true;
            pnlMessage.CssClass = $"alert alert-{type} mt-3";
            lblMessage.Text = message;
        }

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
