using System;
using InventoryManagementSystem.BL;
using InventoryManagementSystem.Helpers;

namespace InventoryManagementSystem.UI.Admin
{
    public partial class AddStationary : AdminBasePage
    {
        private readonly StationaryService stationaryService = new StationaryService();

        protected void Page_Load(object sender, EventArgs e)
        {
            // No need to load anything for Add page
        }

        protected void btnAddStationary_Click(object sender, EventArgs e)
        {
            try
            {
                string locationCode = txtLocationCode.Text.Trim().ToUpper();
                string description = txtDescription.Text.Trim();

                bool success = stationaryService.CreateStationary(locationCode, description);
                if (success)
                {
                    // Redirect back to ManageStationaries with success message
                    Response.Redirect("ManageStationaries.aspx?msg=success");
                }
                else
                {
                    ShowMessage("Failed to add stationary. Location code may already exist.", "alert-danger");
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error adding stationary: " + ex.Message, "alert-danger");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageStationaries.aspx");
        }

        private void ShowMessage(string message, string cssClass)
        {
            lblMessage.Text = message;
            pnlMessage.CssClass = "alert " + cssClass;
            pnlMessage.Visible = true;
        }
    }
}
