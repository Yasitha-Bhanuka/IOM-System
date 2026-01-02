using System;
using System.Web.UI.WebControls;
using InventoryManagementSystem.BL;
using InventoryManagementSystem.Helpers;

namespace InventoryManagementSystem.UI.Admin
{
    public partial class ManageUsers : AdminBasePage
    {
        private UserService userService;

        protected void Page_Load(object sender, EventArgs e)
        {
            userService = new UserService();

            if (!IsPostBack)
            {
                LoadUsers();
            }
        }

        private void LoadUsers()
        {
            try
            {
                var users = userService.GetAllUsers();
                gvUsers.DataSource = users;
                gvUsers.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading users: " + ex.Message, "alert-danger");
            }
        }

        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int userId = Convert.ToInt32(e.CommandArgument);

            try
            {
                if (e.CommandName == "ActivateUser")
                {
                    bool success = userService.ActivateUser(userId);
                    if (success)
                    {
                        ShowMessage("User activated successfully!", "alert-success");
                        LoadUsers();
                    }
                    else
                    {
                        ShowMessage("Failed to activate user. Please try again.", "alert-danger");
                    }
                }
                else if (e.CommandName == "DeactivateUser")
                {
                    bool success = userService.DeactivateUser(userId);
                    if (success)
                    {
                        ShowMessage("User deactivated successfully!", "alert-info");
                        LoadUsers();
                    }
                    else
                    {
                        ShowMessage("Failed to deactivate user. Please try again.", "alert-danger");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error processing request: " + ex.Message, "alert-danger");
            }
        }

        protected void btnBackToDashboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx", false);
        }

        private void ShowMessage(string message, string cssClass)
        {
            lblMessage.Text = message;
            pnlMessage.CssClass = "alert " + cssClass;
            pnlMessage.Visible = true;
        }
    }
}
