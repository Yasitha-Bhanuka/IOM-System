using System;
using InventoryManagementSystem.BL;
using InventoryManagementSystem.Helpers;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.UI.Admin
{
    public partial class AddBranch : AdminBasePage
    {
        private readonly BranchService branchService = new BranchService();

        protected void Page_Load(object sender, EventArgs e)
        {
            // No need to load anything for Add page
        }

        protected void btnAddBranch_Click(object sender, EventArgs e)
        {
            try
            {
                var branch = new Branch
                {
                    BranchName = txtBranchName.Text.Trim(),
                    BranchCode = txtBranchCode.Text.Trim().ToUpper(),
                    City = txtCity.Text.Trim(),
                    PhoneNumber = txtPhoneNumber.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    IsActive = true,
                    CreatedDate = DateTime.Now
                };

                bool success = branchService.CreateBranch(branch);
                if (success)
                {
                    // Redirect back to ManageBranches with success message
                    Response.Redirect("ManageBranches.aspx?msg=success");
                }
                else
                {
                    ShowMessage("Failed to add branch. Branch name or code may already exist.", "alert-danger");
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error adding branch: " + ex.Message, "alert-danger");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageBranches.aspx");
        }

        private void ShowMessage(string message, string cssClass)
        {
            lblMessage.Text = message;
            pnlMessage.CssClass = "alert " + cssClass;
            pnlMessage.Visible = true;
        }
    }
}
