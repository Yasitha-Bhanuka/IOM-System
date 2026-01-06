using System;
using System.Web;
using System.Web.UI;
using WebformsUI.Helpers;

namespace WebformsUI.Pages.Auth
{
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected async void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                var dto = new RegisterUserDto
                {
                    Email = txtEmail.Text.Trim(),
                    Password = txtPassword.Text.Trim(),
                    FullName = txtFullName.Text.Trim(),
                    BranchName = txtBranch.Text.Trim(),
                    RoleName = "User" // Default role
                };

                var result = await ApiClient.RegisterAsync(dto);

                if (result != null) // Assuming success returns string message
                {
                    // Redirect to login
                    Response.Redirect("~/Pages/Auth/Login?registered=true");
                }
                else
                {
                    lblError.Text = "Registration failed. Please try again.";
                    lblError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Error: " + ex.Message;
                lblError.Visible = true;
            }
        }
    }
}
