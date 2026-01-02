using System;
using InventoryManagementSystem.Helpers;

namespace InventoryManagementSystem.UI.Customer
{
    public partial class Dashboard : CustomerBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCustomerInfo();
            }
        }

        private void LoadCustomerInfo()
        {
            try
            {
                lblCustomerName.Text = CurrentUser.FullName ?? CurrentUser.UserEmail;
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error loading customer information: " + ex.Message + "');</script>");
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            AuthService.Logout();
            Response.Redirect("~/UI/Guest/Login.aspx", false);
        }
    }
}
