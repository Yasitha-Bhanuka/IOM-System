using System;
using System.Web;
using System.Web.UI;
using IOMSystem.Contract.DTOs;

public partial class Pages_Common_Dashboard : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserToken"] == null)
        {
            Response.Redirect("~/Pages/Auth/Login.aspx");
        }

        if (!IsPostBack)
        {
             // Try to retrieve user name if available in session, or just generic
             // Assuming we stored UserDto or similar
             // For now, checks token presence.
             // LitUserName.Text = "User"; 
        }
    }
}
