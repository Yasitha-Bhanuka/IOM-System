using System;
using InventoryManagementSystem.BL;
using InventoryManagementSystem.Helpers;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.UI.Admin
{
    public partial class ManageBranches : AdminBasePage
    {
        private BranchService branchService;

        protected void Page_Load(object sender, EventArgs e)
        {
            branchService = new BranchService();

            if (!IsPostBack)
            {
                // Check for success message from AddBranch page
                if (Request.QueryString["msg"] == "success")
                {
                    ShowMessage("Branch added successfully!", "alert-success");
                }
                LoadBranches();
            }
        }

        private void LoadBranches(string searchTerm = "")
        {
            try
            {
                var branches = branchService.GetAllBranches();

                // Apply search filter if provided
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    searchTerm = searchTerm.Trim().ToLower();
                    branches = branches.FindAll(b =>
                        b.BranchName.ToLower().Contains(searchTerm) ||
                        b.BranchCode.ToLower().Contains(searchTerm) ||
                        (b.City != null && b.City.ToLower().Contains(searchTerm))
                    );

                    // Update search results label
                    lblSearchResults.Text = $"Found {branches.Count} branch(es)";
                    lblSearchResults.Visible = true;
                }
                else
                {
                    lblSearchResults.Visible = false;
                }

                gvBranches.DataSource = branches;
                gvBranches.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading branches: " + ex.Message, "alert-danger");
            }
        }

        protected void btnAddNewBranch_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddBranch.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            ViewState["SearchTerm"] = searchTerm;
            LoadBranches(searchTerm);
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            ViewState["SearchTerm"] = "";
            lblSearchResults.Visible = false;
            LoadBranches();
        }

        protected void btnBackToDashboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx", false);
        }


        protected void gvBranches_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int branchId = Convert.ToInt32(e.CommandArgument);

            try
            {
                if (e.CommandName == "ActivateBranch")
                {
                    bool success = branchService.ActivateBranch(branchId);
                    if (success)
                    {
                        ShowMessage("Branch activated successfully!", "alert-success");
                        string searchTerm = ViewState["SearchTerm"] as string;
                        LoadBranches(searchTerm ?? "");
                    }
                    else
                    {
                        ShowMessage("Failed to activate branch. Please try again.", "alert-danger");
                    }
                }
                else if (e.CommandName == "DeactivateBranch")
                {
                    bool success = branchService.DeactivateBranch(branchId);
                    if (success)
                    {
                        ShowMessage("Branch deactivated successfully!", "alert-info");
                        string searchTerm = ViewState["SearchTerm"] as string;
                        LoadBranches(searchTerm ?? "");
                    }
                    else
                    {
                        ShowMessage("Failed to deactivate branch. Please try again.", "alert-danger");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error processing request: " + ex.Message, "alert-danger");
            }
        }

        private void ShowMessage(string message, string cssClass)
        {
            lblMessage.Text = message;
            pnlMessage.CssClass = "alert " + cssClass;
            pnlMessage.Visible = true;
        }
    }
}
