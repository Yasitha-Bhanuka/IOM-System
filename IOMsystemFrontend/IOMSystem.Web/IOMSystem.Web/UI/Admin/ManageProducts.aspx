<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageProducts.aspx.cs"
    Inherits="InventoryManagementSystem.UI.Admin.ManageProducts" %>

    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <title>Manage Products</title>
        <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
        <style>
            body {
                background: #f5f7fa;
                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            }

            .container-custom {
                padding: 30px;
                max-width: 1600px;
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
                padding: 25px;
                box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
                margin-bottom: 20px;
            }

            .btn-add {
                background: #28a745;
                color: white;
                border: none;
                padding: 10px 20px;
                border-radius: 5px;
            }

            .btn-activate {
                background: #28a745;
                color: white;
                border: none;
                padding: 6px 12px;
                border-radius: 5px;
                margin-right: 5px;
                font-size: 14px;
            }

            .btn-deactivate {
                background: #dc3545;
                color: white;
                border: none;
                padding: 6px 12px;
                border-radius: 5px;
                font-size: 14px;
            }

            .btn-back {
                background: #6c757d;
                color: white;
                border: none;
                padding: 10px 20px;
                border-radius: 5px;
            }

            .badge-active {
                background: #28a745;
                color: white;
                padding: 5px 10px;
                border-radius: 5px;
            }

            .badge-inactive {
                background: #dc3545;
                color: white;
                padding: 5px 10px;
                border-radius: 5px;
            }

            .sku-preview {
                background: #e7f3ff;
                border: 2px solid #667eea;
                padding: 10px 15px;
                border-radius: 5px;
                font-weight: bold;
                color: #667eea;
                font-size: 16px;
                text-align: center;
            }

            .sku-label {
                font-size: 12px;
                color: #666;
                margin-bottom: 5px;
            }

            /* Table Improvements */
            .table-responsive-custom {
                overflow-x: auto;
                -webkit-overflow-scrolling: touch;
            }

            .table th {
                white-space: nowrap;
                vertical-align: middle;
                font-weight: 600;
                background-color: #f8f9fa;
                border-bottom: 2px solid #dee2e6;
            }

            .table td {
                vertical-align: middle;
                padding: 12px 8px;
            }

            /* Action buttons styling */
            .action-buttons {
                display: flex;
                gap: 5px;
                flex-wrap: wrap;
                justify-content: flex-start;
            }

            .btn-activate,
            .btn-deactivate {
                min-width: 85px;
                font-size: 13px;
                padding: 6px 12px;
                white-space: nowrap;
            }

            /* Status badge improvements */
            .badge-active,
            .badge-inactive {
                display: inline-block;
                min-width: 70px;
                text-align: center;
                font-size: 12px;
                font-weight: 600;
            }

            /* Responsive adjustments */
            @media (max-width: 1200px) {
                .container-custom {
                    padding: 20px;
                }

                .table {
                    font-size: 13px;
                }

                .table th,
                .table td {
                    padding: 8px 5px;
                }
            }

            @media (max-width: 768px) {
                .container-custom {
                    padding: 15px;
                }

                .table {
                    font-size: 12px;
                }

                .btn-activate,
                .btn-deactivate {
                    font-size: 11px;
                    padding: 5px 8px;
                    min-width: 70px;
                }

                .action-buttons {
                    flex-direction: column;
                }
            }
        </style>
    </head>

    <body>
        <form id="form1" runat="server">
            <div class="container-custom">
                <div class="page-header">
                    <div class="row align-items-center">
                        <div class="col">
                            <h2 class="mb-0">Manage Products</h2>
                        </div>
                        <div class="col-auto">
                            <asp:Button ID="btnAddNewProduct" runat="server" Text="+ Add New Product"
                                CssClass="btn-add me-2" OnClick="btnAddNewProduct_Click" CausesValidation="false" />
                            <asp:Button ID="btnBackToDashboard" runat="server" Text="Back to Dashboard"
                                CssClass="btn-back" OnClick="btnBackToDashboard_Click" CausesValidation="false" />
                        </div>
                    </div>
                </div>

                <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="alert mb-3" role="alert">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </asp:Panel>

                <div class="content-card mb-3">
                    <h5 class="mb-3">Search Products</h5>
                    <div class="row">
                        <div class="col-md-10">
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"
                                placeholder="Search by SKU or Product Name..."></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary w-100"
                                OnClick="btnSearch_Click" CausesValidation="false" />
                        </div>
                    </div>
                    <div class="row mt-2">
                        <div class="col-md-12">
                            <asp:Button ID="btnClearSearch" runat="server" Text="Clear Search"
                                CssClass="btn btn-secondary btn-sm" OnClick="btnClearSearch_Click"
                                CausesValidation="false" />
                            <asp:Label ID="lblSearchResults" runat="server" CssClass="ms-3 text-muted"></asp:Label>
                        </div>
                    </div>
                </div>

                <div class="content-card">
                    <h4 class="mb-3">All Products</h4>
                    <div class="table-responsive-custom">
                        <asp:GridView ID="gvProducts" runat="server" CssClass="table table-striped table-hover"
                            AutoGenerateColumns="False" OnRowCommand="gvProducts_RowCommand"
                            OnRowEditing="gvProducts_RowEditing" OnRowUpdating="gvProducts_RowUpdating"
                            OnRowCancelingEdit="gvProducts_RowCancelingEdit" DataKeyNames="SKU">
                            <Columns>
                                <asp:BoundField DataField="SKU" HeaderText="SKU" ReadOnly="true" />
                                <asp:TemplateField HeaderText="Product Name">
                                    <ItemTemplate>
                                        <%# Eval("ProductName") %>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEditProductName" runat="server"
                                            Text='<%# Bind("ProductName") %>' CssClass="form-control" MaxLength="100">
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="LocationCode" HeaderText="Location" ReadOnly="true" />
                                <asp:TemplateField HeaderText="Price">
                                    <ItemTemplate>
                                        Rs.<%# Eval("Price", "{0:F2}" ) %>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEditPrice" runat="server" Text='<%# Bind("Price") %>'
                                            CssClass="form-control" TextMode="Number" step="0.01"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Stock">
                                    <ItemTemplate>
                                        <span
                                            style='color: <%# (int)Eval("StockQuantity") < (int)Eval("MinStockThreshold") ? "red" : 
                                            ((int)Eval("StockQuantity") < (int)Eval("MinStockThreshold") * 2 ? "orange" : "green") %>; font-weight: bold;'>
                                            <%# Eval("StockQuantity") %>
                                        </span>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEditStock" runat="server"
                                            Text='<%# Bind("StockQuantity") %>' CssClass="form-control"
                                            TextMode="Number"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Min Threshold">
                                    <ItemTemplate>
                                        <%# Eval("MinStockThreshold") %>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEditMinThreshold" runat="server"
                                            Text='<%# Bind("MinStockThreshold") %>' CssClass="form-control"
                                            TextMode="Number"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CreatedDate" HeaderText="Created Date"
                                    DataFormatString="{0:yyyy-MM-dd}" ReadOnly="true" />
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <%# (bool)Eval("IsActive") ? "<span class='badge-active'>Active</span>"
                                            : "<span class='badge-inactive'>Inactive</span>" %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <div class="action-buttons">
                                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn-activate"
                                                CommandName="Edit" CausesValidation="false" />
                                            <asp:Button ID="btnActivate" runat="server" Text="Activate"
                                                CssClass="btn-activate" CommandName="ActivateProduct"
                                                CommandArgument='<%# Eval("SKU") %>'
                                                Visible='<%# !(bool)Eval("IsActive") %>' CausesValidation="false" />
                                            <asp:Button ID="btnDeactivate" runat="server" Text="Deactivate"
                                                CssClass="btn-deactivate" CommandName="DeactivateProduct"
                                                CommandArgument='<%# Eval("SKU") %>'
                                                Visible='<%# (bool)Eval("IsActive") %>'
                                                OnClientClick="return confirm('Are you sure you want to deactivate this product?');"
                                                CausesValidation="false" />
                                        </div>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <div class="action-buttons">
                                            <asp:Button ID="btnUpdate" runat="server" Text="Update"
                                                CssClass="btn-activate" CommandName="Update" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                                CssClass="btn-deactivate" CommandName="Cancel"
                                                CausesValidation="false" />
                                        </div>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="text-center text-muted py-4">
                                    <h5>No products found</h5>
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </form>
    </body>

    </html>