using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.Script.Serialization; // Ideally use NewtonSoft, but using System.Web.Script.Serialization for built-in support

public partial class Login : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserToken"] != null)
        {
            // Redirect if already logged in (Dashboard to be created)
            // Response.Redirect("~/Dashboard.aspx");
        }
    }

    protected async void btnLogin_Click(object sender, EventArgs e)
    {
        string email = txtEmail.Text.Trim();
        string password = txtPassword.Text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            lblMessage.Text = "Please enter email and password.";
            lblMessage.Visible = true;
            return;
        }

        try
        {
            var loginDto = new IOMSystem.Contract.DTOs.LoginDto
            {
                Email = email,
                Password = password
            };

            var apiService = new IOMSystem.UI.Services.ApiService();
            // Note: Verify endpoint matches your API controller route
            var user = await apiService.LoginAsync(loginDto);

            if (user != null)
            {
                Session["UserEmail"] = user.UserEmail;
                Session["UserRole"] = user.RoleName;
                Session["UserToken"] = "dummy-token"; // Replace if API returns JWT separately

                lblMessage.Text = "Login successful!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Visible = true;

                // Response.Redirect("~/Dashboard.aspx");
            }
            else
            {
                lblMessage.Text = "Invalid login credentials.";
                lblMessage.Visible = true;
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error: " + ex.Message;
            lblMessage.Visible = true;
        }
    }
}
