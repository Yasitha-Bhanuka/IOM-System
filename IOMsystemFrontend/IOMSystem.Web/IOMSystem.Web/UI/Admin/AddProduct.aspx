<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs"
    Inherits="InventoryManagementSystem.UI.Admin.AddProduct" %>

    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <title>Add Product - Inventory Management System</title>
        <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
        <style>
            body {
                background: #f5f7fa;
                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            }

            .container-custom {
                padding: 30px;
                max-width: 1000px;
                margin: 0 auto;
            }

            .page-header {
                background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
                color: white;
                padding: 25px;
                border-radius: 10px;
                margin-bottom: 30px;
            }

            .content-card {
                background: white;
                border-radius: 10px;
                padding: 30px;
                box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
            }

            .btn-add {
                background: #28a745;
                color: white;
                border: none;
                padding: 12px 30px;
                border-radius: 5px;
                font-weight: 600;
            }

            .btn-add:hover {
                background: #218838;
            }

            .btn-cancel {
                background: #6c757d;
                color: white;
                border: none;
                padding: 12px 30px;
                border-radius: 5px;
                margin-right: 10px;
            }

            .btn-cancel:hover {
                background: #5a6268;
            }

            .sku-preview {
                background: #e7f3ff;
                border: 2px solid #667eea;
                padding: 15px 20px;
                border-radius: 5px;
                font-weight: bold;
                color: #667eea;
                font-size: 18px;
                text-align: center;
            }

            .sku-label {
                font-size: 13px;
                color: #666;
                margin-bottom: 8px;
                font-weight: 600;
            }
        </style>
    </head>

    <body>
        <form id="form1" runat="server">
            <div class="container-custom">
                <div class="page-header">
                    <h2 class="mb-0">Add New Product</h2>
                    <p class="mb-0">Create a new product in the inventory system</p>
                </div>

                <div class="content-card">
                    <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="alert mb-3" role="alert">
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </asp:Panel>

                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Product Name *</label>
                            <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control"
                                placeholder="Enter product name" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvProductName" runat="server"
                                ControlToValidate="txtProductName" ErrorMessage="Product name is required"
                                CssClass="text-danger" Display="Dynamic" ValidationGroup="AddProduct">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Stationary Location *</label>
                            <asp:DropDownList ID="ddlStationary" runat="server" CssClass="form-control"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlStationary_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvStationary" runat="server"
                                ControlToValidate="ddlStationary" InitialValue=""
                                ErrorMessage="Please select a stationary location" CssClass="text-danger"
                                Display="Dynamic" ValidationGroup="AddProduct"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Product ID * (e.g., 001, 002)</label>
                            <asp:TextBox ID="txtProductID" runat="server" CssClass="form-control" placeholder="001"
                                MaxLength="10" AutoPostBack="true" OnTextChanged="txtProductID_TextChanged">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvProductID" runat="server"
                                ControlToValidate="txtProductID" ErrorMessage="Product ID is required"
                                CssClass="text-danger" Display="Dynamic" ValidationGroup="AddProduct">
                            </asp:RequiredFieldValidator>
                            <small class="text-muted">Suggested: <asp:Label ID="lblSuggestedID" runat="server"
                                    Text="001"></asp:Label></small>
                        </div>
                        <div class="col-md-6 mb-3">
                            <label class="form-label sku-label">Generated SKU</label>
                            <div class="sku-preview">
                                <asp:Label ID="lblSKUPreview" runat="server" Text="Select location and enter ID">
                                </asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Price (Rs.) *</label>
                            <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" placeholder="0.00"
                                TextMode="Number" step="0.01"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPrice" runat="server" ControlToValidate="txtPrice"
                                ErrorMessage="Price is required" CssClass="text-danger" Display="Dynamic"
                                ValidationGroup="AddProduct"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="rvPrice" runat="server" ControlToValidate="txtPrice"
                                MinimumValue="0.01" MaximumValue="999999" Type="Double"
                                ErrorMessage="Price must be greater than 0" CssClass="text-danger" Display="Dynamic"
                                ValidationGroup="AddProduct"></asp:RangeValidator>
                        </div>
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Stock Quantity *</label>
                            <asp:TextBox ID="txtStockQuantity" runat="server" CssClass="form-control" placeholder="0"
                                TextMode="Number"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvStockQuantity" runat="server"
                                ControlToValidate="txtStockQuantity" ErrorMessage="Stock quantity is required"
                                CssClass="text-danger" Display="Dynamic" ValidationGroup="AddProduct">
                            </asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="rvStockQuantity" runat="server" ControlToValidate="txtStockQuantity"
                                MinimumValue="0" MaximumValue="999999" Type="Integer"
                                ErrorMessage="Stock quantity must be 0 or greater" CssClass="text-danger"
                                Display="Dynamic" ValidationGroup="AddProduct"></asp:RangeValidator>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12 mb-3">
                            <label class="form-label">Min Stock Threshold *</label>
                            <asp:TextBox ID="txtMinStockThreshold" runat="server" CssClass="form-control"
                                placeholder="10" TextMode="Number"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMinStockThreshold" runat="server"
                                ControlToValidate="txtMinStockThreshold" ErrorMessage="Min stock threshold is required"
                                CssClass="text-danger" Display="Dynamic" ValidationGroup="AddProduct">
                            </asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="rvMinStockThreshold" runat="server"
                                ControlToValidate="txtMinStockThreshold" MinimumValue="0" MaximumValue="999999"
                                Type="Integer" ErrorMessage="Min stock threshold must be 0 or greater"
                                CssClass="text-danger" Display="Dynamic" ValidationGroup="AddProduct">
                            </asp:RangeValidator>
                            <small class="text-muted">Alert when stock falls below this level</small>
                        </div>
                    </div>

                    <div class="mt-4">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn-cancel"
                            OnClick="btnCancel_Click" CausesValidation="false" />
                        <asp:Button ID="btnAddProduct" runat="server" Text="Add Product" CssClass="btn-add"
                            OnClick="btnAddProduct_Click" ValidationGroup="AddProduct" />
                    </div>
                </div>
            </div>
        </form>
    </body>

    </html>