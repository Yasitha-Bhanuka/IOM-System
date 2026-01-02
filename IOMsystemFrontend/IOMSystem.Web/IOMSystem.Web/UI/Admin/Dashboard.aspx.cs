using System;
using InventoryManagementSystem.BL;
using InventoryManagementSystem.Helpers;

namespace InventoryManagementSystem.UI.Admin
{
    public partial class Dashboard : AdminBasePage
    {
        private RegistrationService registrationService;
        private UserService userService;
        private BranchService branchService;
        private ProductService productService;
        private OrderService orderService;

        protected void Page_Load(object sender, EventArgs e)
        {
            registrationService = new RegistrationService();
            userService = new UserService();
            branchService = new BranchService();
            productService = new ProductService();
            orderService = new OrderService();

            if (!IsPostBack)
            {
                LoadDashboardData();
            }
        }

        private void LoadDashboardData()
        {
            try
            {
                // Display admin name
                lblAdminName.Text = CurrentUser.FullName ?? CurrentUser.UserEmail;

                // Load statistics
                var pendingRequests = registrationService.GetPendingRequests();
                lblPendingRequests.Text = pendingRequests.Count.ToString();

                var activeUsers = userService.GetActiveUsers();
                lblActiveUsers.Text = activeUsers.Count.ToString();

                var branches = branchService.GetAllBranches();
                lblTotalBranches.Text = branches.Count.ToString();

                // Load low stock count (US-004)
                var lowStockCount = productService.GetLowStockCount();
                lblLowStockCount.Text = lowStockCount.ToString();

                // Load Pending Orders Count
                lblPendingOrders.Text = orderService.GetPendingOrdersCount().ToString();
            }
            catch (Exception ex)
            {
                // Log error
                Response.Write("<script>alert('Error loading dashboard: " + ex.Message + "');</script>");
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            AuthService.Logout();
            Response.Redirect("~/UI/Guest/Login.aspx", false);
        }
    }
}
