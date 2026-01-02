using System;

namespace InventoryManagementSystem.UI.Guest
{
    public partial class AccessDenied : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnBackToLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Guest/Login.aspx", false);
        }
    }
}
