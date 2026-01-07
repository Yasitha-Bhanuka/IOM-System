using System;
using System.Web;
using System.Web.UI;
using IOMSystem.Contract.DTOs;
using IOMSystem.UI.Services;

public partial class Pages_Auth_Register : Page
{
    private readonly ApiService _apiService = new ApiService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserToken"] != null)
        {
            Response.Redirect("~/Pages/Common/Dashboard.aspx");
        }
    }

    protected async void RegisterUser(object sender, EventArgs e)
    {
        if (IsValid)
        {
            var dto = new CreateRegistrationRequestDto
            {
                UserEmail = Email.Text,
                FullName = FullName.Text,
                Password = Password.Text,
                // Default to a known branch for now, or add UI logic for it
                BranchCode = "HEAD"
            };

            try
            {
                bool success = await _apiService.CreateRegistrationRequestAsync(dto);

                if (success)
                {
                    lblMessage.CssClass = "text-success";
                    lblMessage.Text = "Registration successful! Please wait for approval.";
                    // Optional: Clear fields
                    FullName.Text = "";
                    Email.Text = "";
                }
                else
                {
                    lblMessage.CssClass = "text-danger";
                    lblMessage.Text = "Registration failed. Email might be in use.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
        }
    }
}
