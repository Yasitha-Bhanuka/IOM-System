using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using WebformsUI.Helpers;

namespace WebformsUI.Pages.Auth
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (User.Identity.IsAuthenticated)
                {
                    Response.Redirect("~/Pages/Dashboard/Default");
                }
            }
        }

        protected async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var email = txtEmail.Text.Trim();
                var password = txtPassword.Text.Trim();

                var user = await ApiClient.LoginAsync(email, password);

                if (user != null)
                {
                    // Successful login
                    FormsAuthentication.SetAuthCookie(user.UserEmail, false);
                    Session["UserRole"] = user.RoleName;
                    Session["UserName"] = user.FullName;

                    Response.Redirect("~/Pages/Dashboard/Default");
                }
                else
                {
                    lblMessage.Text = "Invalid email or password.";
                    lblMessage.CssClass = "text-danger";
                    lblMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred: " + ex.Message;
                lblMessage.CssClass = "text-danger";
                lblMessage.Visible = true;
            }
        }
    }
}
