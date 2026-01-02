using System;
using System.Web.UI.WebControls;
using InventoryManagementSystem.BL;
using InventoryManagementSystem.Helpers;

namespace InventoryManagementSystem.UI.Admin
{
    public partial class PendingRegistrations : AdminBasePage
    {
        private RegistrationService registrationService;

        protected void Page_Load(object sender, EventArgs e)
        {
            registrationService = new RegistrationService();

            if (!IsPostBack)
            {
                LoadPendingRequests();
            }
        }

        private void LoadPendingRequests()
        {
            try
            {
                var pendingRequests = registrationService.GetPendingRequests();
                gvPendingRequests.DataSource = pendingRequests;
                gvPendingRequests.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading pending requests: " + ex.Message, "alert-danger");
            }
        }

        protected void gvPendingRequests_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int requestId = Convert.ToInt32(e.CommandArgument);

            try
            {
                if (e.CommandName == "ApproveRequest")
                {
                    bool success = registrationService.ApproveRequest(requestId, CurrentUser.UserId);
                    if (success)
                    {
                        ShowMessage("Registration request approved successfully!", "alert-success");
                        LoadPendingRequests();
                    }
                    else
                    {
                        ShowMessage("Failed to approve request. Please try again.", "alert-danger");
                    }
                }
                else if (e.CommandName == "RejectRequest")
                {
                    bool success = registrationService.RejectRequest(requestId, CurrentUser.UserId, "Rejected by admin");
                    if (success)
                    {
                        ShowMessage("Registration request rejected.", "alert-info");
                        LoadPendingRequests();
                    }
                    else
                    {
                        ShowMessage("Failed to reject request. Please try again.", "alert-danger");
                    }
                }
                else if (e.CommandName == "DeleteRequest")
                {
                    bool success = registrationService.DeleteRequest(requestId);
                    if (success)
                    {
                        ShowMessage("Registration request deleted successfully!", "alert-success");
                        LoadPendingRequests();
                    }
                    else
                    {
                        ShowMessage("Failed to delete request. Please try again.", "alert-danger");
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
