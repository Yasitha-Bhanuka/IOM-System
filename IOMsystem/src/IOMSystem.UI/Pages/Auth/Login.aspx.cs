using System;
using System.Web;
using System.Web.UI;
using IOMSystem.Contract.DTOs;
using IOMSystem.UI.Services;

public partial class Pages_Auth_Login : Page
{
    private readonly ApiService _apiService = new ApiService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserToken"] != null)
        {
            Response.Redirect("~/Pages/Common/Dashboard.aspx");
        }
    }

    protected async void LogIn(object sender, EventArgs e)
    {
        if (IsValid)
        {
            var loginDto = new LoginDto
            {
                Email = Email.Text,
                Password = Password.Text
            };

            try 
            {
                var user = await _apiService.LoginAsync(loginDto);
                if (user != null && !string.IsNullOrEmpty(user.Token))
                {
                    Session["UserToken"] = user.Token;
                    Session["UserEmail"] = user.UserEmail;
                    Session["UserRole"] = user.RoleName;
                    Response.Redirect("~/Pages/Common/Dashboard.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    lblMessage.Text = "Invalid login attempt.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
        }
    }
}
