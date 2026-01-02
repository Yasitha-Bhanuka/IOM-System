using System;
using System.Web.UI.WebControls;
using InventoryManagementSystem.BL;
using InventoryManagementSystem.Helpers;

namespace InventoryManagementSystem.UI.Admin
{
    public partial class ManageStationaries : AdminBasePage
    {
        private StationaryService stationaryService;

        protected void Page_Load(object sender, EventArgs e)
        {
            stationaryService = new StationaryService();

            if (!IsPostBack)
            {
                // Check for success message from AddStationary page
                if (Request.QueryString["msg"] == "success")
                {
                    ShowMessage("Stationary added successfully!", "alert-success");
                }
                LoadStationaries();
            }
        }

        private void LoadStationaries(string searchTerm = "")
        {
            try
            {
                var stationaries = stationaryService.GetAllStationaries();

                // Apply search filter if provided
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    searchTerm = searchTerm.Trim().ToLower();
                    stationaries = stationaries.FindAll(s =>
                        s.LocationCode.ToLower().Contains(searchTerm) ||
                        (s.Description != null && s.Description.ToLower().Contains(searchTerm))
                    );

                    // Update search results label
                    lblSearchResults.Text = $"Found {stationaries.Count} stationary location(s)";
                    lblSearchResults.Visible = true;
                }
                else
                {
                    lblSearchResults.Visible = false;
                }

                gvStationaries.DataSource = stationaries;
                gvStationaries.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading stationaries: " + ex.Message, "alert-danger");
            }
        }

        protected void btnAddNewStationary_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddStationary.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            ViewState["SearchTerm"] = searchTerm;
            LoadStationaries(searchTerm);
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            ViewState["SearchTerm"] = "";
            lblSearchResults.Visible = false;
            LoadStationaries();
        }

        protected void gvStationaries_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string locationCode = e.CommandArgument.ToString();

            try
            {
                if (e.CommandName == "ActivateStationary")
                {
                    bool success = stationaryService.ActivateStationary(locationCode);
                    if (success)
                    {
                        ShowMessage("Stationary activated successfully!", "alert-success");
                        string searchTerm = ViewState["SearchTerm"] as string;
                        LoadStationaries(searchTerm ?? "");
                    }
                    else
                    {
                        ShowMessage("Failed to activate stationary. Please try again.", "alert-danger");
                    }
                }
                else if (e.CommandName == "DeactivateStationary")
                {
                    bool success = stationaryService.DeleteStationary(locationCode);
                    if (success)
                    {
                        ShowMessage("Stationary deactivated successfully!", "alert-info");
                        string searchTerm = ViewState["SearchTerm"] as string;
                        LoadStationaries(searchTerm ?? "");
                    }
                    else
                    {
                        ShowMessage("Failed to deactivate stationary. Please try again.", "alert-danger");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error processing request: " + ex.Message, "alert-danger");
            }
        }

        protected void gvStationaries_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvStationaries.EditIndex = e.NewEditIndex;
            string searchTerm = ViewState["SearchTerm"] as string;
            LoadStationaries(searchTerm ?? "");
        }

        protected void gvStationaries_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                string locationCode = gvStationaries.DataKeys[e.RowIndex].Value.ToString();
                TextBox txtEditDescription = (TextBox)gvStationaries.Rows[e.RowIndex].FindControl("txtEditDescription");
                string description = txtEditDescription.Text.Trim();

                bool success = stationaryService.UpdateStationary(locationCode, description);
                if (success)
                {
                    ShowMessage("Stationary updated successfully!", "alert-success");
                    gvStationaries.EditIndex = -1;
                    string searchTerm = ViewState["SearchTerm"] as string;
                    LoadStationaries(searchTerm ?? "");
                }
                else
                {
                    ShowMessage("Failed to update stationary. Please try again.", "alert-danger");
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error updating stationary: " + ex.Message, "alert-danger");
            }
        }

        protected void gvStationaries_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvStationaries.EditIndex = -1;
            string searchTerm = ViewState["SearchTerm"] as string;
            LoadStationaries(searchTerm ?? "");
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
