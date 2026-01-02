using System;
using System.Web.UI.WebControls;
using InventoryManagementSystem.BL;
using InventoryManagementSystem.Helpers;

namespace InventoryManagementSystem.UI.Admin
{
    public partial class AddProduct : AdminBasePage
    {
        private readonly StationaryService stationaryService = new StationaryService();
        private readonly ProductService productService = new ProductService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStationaries();
            }
        }

        private void LoadStationaries()
        {
            try
            {
                var stationaries = stationaryService.GetActiveStationaries();
                ddlStationary.DataSource = stationaries;
                ddlStationary.DataTextField = "LocationCode";
                ddlStationary.DataValueField = "LocationCode";
                ddlStationary.DataBind();
                ddlStationary.Items.Insert(0, new ListItem("-- Select Location --", ""));
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading stationaries: " + ex.Message, "alert-danger");
            }
        }

        protected void ddlStationary_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSKUPreview();
            UpdateSuggestedProductID();
        }

        protected void txtProductID_TextChanged(object sender, EventArgs e)
        {
            UpdateSKUPreview();
        }

        private void UpdateSKUPreview()
        {
            string locationCode = ddlStationary.SelectedValue;
            string productID = txtProductID.Text.Trim().ToUpper();

            if (!string.IsNullOrEmpty(locationCode) && !string.IsNullOrEmpty(productID))
            {
                lblSKUPreview.Text = locationCode + productID;
            }
            else if (!string.IsNullOrEmpty(locationCode))
            {
                lblSKUPreview.Text = locationCode + "___";
            }
            else
            {
                lblSKUPreview.Text = "Select location and enter ID";
            }
        }

        private void UpdateSuggestedProductID()
        {
            string locationCode = ddlStationary.SelectedValue;
            if (!string.IsNullOrEmpty(locationCode))
            {
                string suggestedID = productService.GetSuggestedProductID(locationCode);
                lblSuggestedID.Text = suggestedID;
            }
            else
            {
                lblSuggestedID.Text = "001";
            }
        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                string productName = txtProductName.Text.Trim();
                string locationCode = ddlStationary.SelectedValue;
                string productID = txtProductID.Text.Trim().ToUpper();
                decimal price = decimal.Parse(txtPrice.Text.Trim());
                int stockQuantity = int.Parse(txtStockQuantity.Text.Trim());
                int minStockThreshold = int.Parse(txtMinStockThreshold.Text.Trim());

                bool success = productService.CreateProduct(productName, locationCode, productID, price, stockQuantity, minStockThreshold);
                if (success)
                {
                    // Redirect back to ManageProducts with success message
                    Response.Redirect("ManageProducts.aspx?msg=success");
                }
                else
                {
                    ShowMessage("Failed to add product. SKU may already exist or invalid data provided.", "alert-danger");
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error adding product: " + ex.Message, "alert-danger");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageProducts.aspx");
        }

        private void ShowMessage(string message, string cssClass)
        {
            lblMessage.Text = message;
            pnlMessage.CssClass = "alert " + cssClass;
            pnlMessage.Visible = true;
        }
    }
}
