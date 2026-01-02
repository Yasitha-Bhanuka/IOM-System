using System;
using System.Web.UI.WebControls;
using InventoryManagementSystem.BL;
using InventoryManagementSystem.Helpers;

namespace InventoryManagementSystem.UI.Admin
{
    public partial class ManageProducts : AdminBasePage
    {
        private ProductService productService;
        private StationaryService stationaryService;

        protected void Page_Load(object sender, EventArgs e)
        {
            productService = new ProductService();
            stationaryService = new StationaryService();

            if (!IsPostBack)
            {
                // Check for success message from AddProduct page
                if (Request.QueryString["msg"] == "success")
                {
                    ShowMessage("Product added successfully!", "alert-success");
                }
                LoadProducts();
            }
        }

        private void LoadProducts(string searchTerm = "")
        {
            try
            {
                var products = productService.GetAllProducts();

                // Apply search filter if provided
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    searchTerm = searchTerm.Trim().ToLower();
                    products = products.FindAll(p =>
                        p.SKU.ToLower().Contains(searchTerm) ||
                        p.ProductName.ToLower().Contains(searchTerm)
                    );

                    // Update search results label
                    lblSearchResults.Text = $"Found {products.Count} product(s)";
                    lblSearchResults.Visible = true;
                }
                else
                {
                    lblSearchResults.Visible = false;
                }

                gvProducts.DataSource = products;
                gvProducts.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading products: " + ex.Message, "alert-danger");
            }
        }

        protected void btnAddNewProduct_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddProduct.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            ViewState["SearchTerm"] = searchTerm;
            LoadProducts(searchTerm);
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            ViewState["SearchTerm"] = " ";
            lblSearchResults.Visible = false;
            LoadProducts();
        }

        protected void gvProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string sku = e.CommandArgument.ToString();

            try
            {
                if (e.CommandName == "ActivateProduct")
                {
                    bool success = productService.ActivateProduct(sku);
                    if (success)
                    {
                        ShowMessage("Product activated successfully!", "alert-success");
                        // Maintain search filter after operation
                        string searchTerm = ViewState["SearchTerm"] as string;
                        LoadProducts(searchTerm ?? "");
                    }
                    else
                    {
                        ShowMessage("Failed to activate product. Please try again.", "alert-danger");
                    }
                }
                else if (e.CommandName == "DeactivateProduct")
                {
                    bool success = productService.DeleteProduct(sku);
                    if (success)
                    {
                        ShowMessage("Product deactivated successfully!", "alert-info");
                        // Maintain search filter after operation
                        string searchTerm = ViewState["SearchTerm"] as string;
                        LoadProducts(searchTerm ?? "");
                    }
                    else
                    {
                        ShowMessage("Failed to deactivate product. Please try again.", "alert-danger");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error processing request: " + ex.Message, "alert-danger");
            }
        }

        protected void gvProducts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvProducts.EditIndex = e.NewEditIndex;
            // Maintain search filter when editing
            string searchTerm = ViewState["SearchTerm"] as string;
            LoadProducts(searchTerm ?? "");
        }

        protected void gvProducts_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                string sku = gvProducts.DataKeys[e.RowIndex].Value.ToString();
                TextBox txtEditProductName = (TextBox)gvProducts.Rows[e.RowIndex].FindControl("txtEditProductName");
                TextBox txtEditPrice = (TextBox)gvProducts.Rows[e.RowIndex].FindControl("txtEditPrice");
                TextBox txtEditStock = (TextBox)gvProducts.Rows[e.RowIndex].FindControl("txtEditStock");
                TextBox txtEditMinThreshold = (TextBox)gvProducts.Rows[e.RowIndex].FindControl("txtEditMinThreshold");

                string productName = txtEditProductName.Text.Trim();
                decimal price = decimal.Parse(txtEditPrice.Text.Trim());
                int stockQuantity = int.Parse(txtEditStock.Text.Trim());
                int minStockThreshold = int.Parse(txtEditMinThreshold.Text.Trim());

                bool success = productService.UpdateProduct(sku, productName, price, stockQuantity, minStockThreshold);
                if (success)
                {
                    ShowMessage("Product updated successfully!", "alert-success");
                    gvProducts.EditIndex = -1;
                    // Maintain search filter after update
                    string searchTerm = ViewState["SearchTerm"] as string;
                    LoadProducts(searchTerm ?? "");
                }
                else
                {
                    ShowMessage("Failed to update product. Please try again.", "alert-danger");
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error updating product: " + ex.Message, "alert-danger");
            }
        }

        protected void gvProducts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvProducts.EditIndex = -1;
            // Maintain search filter when canceling edit
            string searchTerm = ViewState["SearchTerm"] as string;
            LoadProducts(searchTerm ?? "");
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
